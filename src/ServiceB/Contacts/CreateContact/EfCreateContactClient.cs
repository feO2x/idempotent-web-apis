using System;
using System.Threading;
using System.Threading.Tasks;
using Light.DatabaseAccess.EntityFrameworkCore;
using Light.GuardClauses;
using Npgsql;
using ServiceB.DatabaseAccess;
using Shared.Contacts;

namespace ServiceB.Contacts.CreateContact;

public sealed class EfCreateContactClient : EfClient<ServiceBDbContext>, ICreateContactClient
{
    public EfCreateContactClient(ServiceBDbContext dbContext) : base(dbContext) { }

    public async Task<UpsertResult> UpsertContactAsync(Contact contact, CancellationToken cancellationToken = default)
    {
        const string sql =
            """
            INSERT INTO contacts (id, name, email, phone_number)
            VALUES ($1, $2, $3, $4)
            ON CONFLICT (id) DO UPDATE
            SET name = excluded.name,
                email = excluded.email,
                phone_number = excluded.phone_number
            RETURNING (xmax = 0) AS inserted
            """;
        await using var command = await CreateCommandAsync<NpgsqlCommand>(sql, cancellationToken);
        command.Parameters.Add(new NpgsqlParameter<Guid> { TypedValue = contact.Id });
        command.Parameters.Add(new () { Value = contact.Name });
        command.Parameters.Add(new () { Value = (object?) contact.Email ?? DBNull.Value });
        command.Parameters.Add(new () { Value = (object?) contact.PhoneNumber ?? DBNull.Value });
        var wasInserted = (await command.ExecuteScalarAsync(cancellationToken)).MustBeOfType<bool>();
        return wasInserted ? UpsertResult.Inserted : UpsertResult.Updated;
    }
}
