using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServiceB.DatabaseAccess;
using Shared.Contacts;

namespace ServiceB.Contacts.Shared;

public static class CommonQueries
{
    public static Task<Contact?> GetContactAsync(
        this ServiceBDbContext dbContext,
        int id,
        CancellationToken cancellationToken = default
    ) =>
        dbContext.Contacts.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
}
