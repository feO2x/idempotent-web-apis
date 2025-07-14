using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Serilog;
using Shared.CommonDtoValidation;
using Shared.Model;

namespace ServiceB.Contacts.CreateContact;

public static class CreateContactEndpoint
{
    public static void MapCreateContactEndpoint(this WebApplication app)
    {
        app.MapPost("/api/contacts", CreateContact)
           .WithName("CreateContact")
           .WithTags("Contacts")
           .WithSummary("CreateContact")
           .WithDescription("Creates a new contact")
           .Produces<Contact>(StatusCodes.Status201Created)
           .ProducedBadRequestProblemDetails()
           .Produces(StatusCodes.Status500InternalServerError);
    }

    public static async Task<IResult> CreateContact(
        CreateContactDto dto,
        CreateContactDtoValidator validator,
        ICreateContactSession session,
        ILogger logger,
        CancellationToken cancellationToken = default
    )
    {
        if (validator.CheckForErrors(dto, out IResult? result))
        {
            return result;
        }

        var contact = new Contact
        {
            Name = dto.Name,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber
        };
        session.AddContact(contact);
        await session.SaveChangesAsync(cancellationToken);
        logger.Information("Created contact {@Contact}", contact);
        return TypedResults.Created($"/api/contacts/{contact.Id}", contact);
    }
}
