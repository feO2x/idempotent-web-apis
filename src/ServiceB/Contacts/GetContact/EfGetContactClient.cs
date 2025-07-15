using System.Threading;
using System.Threading.Tasks;
using Light.DatabaseAccess.EntityFrameworkCore;
using ServiceB.Contacts.Shared;
using ServiceB.DatabaseAccess;
using Shared.Contacts;

namespace ServiceB.Contacts.GetContact;

public sealed class EfGetContactClient : EfClient<ServiceBDbContext>, IGetContactClient
{
    public EfGetContactClient(ServiceBDbContext dbContext) : base(dbContext) { }

    public Task<Contact?> GetContactAsync(int id, CancellationToken cancellationToken = default) =>
        DbContext.GetContactAsync(id, cancellationToken);
}
