using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Serilog;
using Shared.CommonDtoValidation;
using Shared.Model;

namespace ServiceB.Contacts.UpdateContact;

public static class UpdateContactEndpoint
{
    public static void MapUpdateContactEndpoint(this WebApplication app) =>
        app.MapPut("/api/contacts/", UpdateContact)
           .WithName("UpdateContact")
           .WithTags("Contacts")
           .WithSummary("UpdateContact")
           .WithDescription("Updates a contact")
           .Produces(StatusCodes.Status204NoContent)
           .ProducedBadRequestProblemDetails()
           .Produces(StatusCodes.Status404NotFound)
           .Produces(StatusCodes.Status500InternalServerError);

    public static async Task<IResult> UpdateContact(
        Contact dto,
        ContactValidator validator,
        IUpdateContactSession session,
        ILogger logger,
        CancellationToken cancellationToken = default
    )
    {
        if (validator.CheckForErrors(dto, out IResult? result))
        {
            return result;
        }

        var contact = await session.GetContactAsync(dto.Id, cancellationToken);
        if (contact is null)
        {
            return TypedResults.NotFound();
        }

        contact.Name = dto.Name;
        contact.Email = dto.Email;
        contact.PhoneNumber = dto.PhoneNumber;
        await session.SaveChangesAsync(cancellationToken);
        logger.Information("Updated contact {@Contact}", contact);
        return TypedResults.NoContent();
    }
}
