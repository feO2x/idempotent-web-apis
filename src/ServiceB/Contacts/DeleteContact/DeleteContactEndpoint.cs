using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Light.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Serilog;
using Shared.CommonDtoValidation;

namespace ServiceB.Contacts.DeleteContact;

public static class DeleteContactEndpoint
{
    public static void MapDeleteContactEndpoint(this WebApplication app)
    {
        app.MapDelete("/api/contacts/{id:guid}", DeleteContact)
           .WithName("DeleteContact")
           .WithTags("Contacts")
           .WithSummary("DeleteContact")
           .WithDescription("Deletes a contact")
           .Produces(StatusCodes.Status204NoContent)
           .ProducedBadRequestProblemDetails()
           .Produces(StatusCodes.Status404NotFound)
           .Produces(StatusCodes.Status500InternalServerError);
    }

    public static async Task<IResult> DeleteContact(
        ValidationContext validationContext,
        IDeleteContactSession dbSession,
        [Description("ID of the contact to delete - must not be empty")] Guid id,
        ILogger logger,
        CancellationToken cancellationToken = default
    )
    {
        if (validationContext.CheckIdForErrors(id, out var result))
        {
            return result;
        }

        var contact = await dbSession.GetContactAsync(id, cancellationToken);
        if (contact is null)
        {
            return TypedResults.NotFound();
        }

        dbSession.RemoveContact(contact);
        await dbSession.SaveChangesAsync(cancellationToken);
        logger.Information("Deleted contact {@Contact}", contact);
        return TypedResults.NoContent();
    }
}
