using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using ServiceA.HttpAccess;
using Shared.CommonDtoValidation;
using Shared.Contacts;

namespace ServiceA.Contacts.CreateContact;

public static class CreateContactEndpoint
{
    public static void MapCreateContactEndpoint(this WebApplication app) =>
        app.MapPost("/api/contacts", CreateContact)
           .WithName("CreateContact")
           .WithTags("Contacts")
           .WithSummary("CreateContact")
           .WithDescription("Creates a new contact")
           .Produces<Contact>(StatusCodes.Status201Created)
           .ProducedBadRequestProblemDetails()
           .Produces(StatusCodes.Status500InternalServerError);

    public static async Task<IResult> CreateContact(
        CreateContactDtoWithErrors dto,
        CreateContactDtoWithErrorsValidator validator,
        IChaosClientFactory<ICreateContactClient> clientFactory,
        CancellationToken cancellationToken = default
    )
    {
        if (validator.CheckForErrors(dto, out IResult? result))
        {
            return result;
        }

        var createContactDto = dto.ToCreateContactDto();
        await using var client = clientFactory.CreateClient(
            dto.NumberOfErrorsBeforeServiceCall,
            dto.NumberOfErrorsAfterServiceCall
        );
        var contact = await client.CreateContactAsync(createContactDto, cancellationToken);
        return TypedResults.Created($"/api/contacts/{contact.Id}", contact);
    }
}
