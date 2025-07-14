using System.Threading;
using System.Threading.Tasks;
using Light.DatabaseAccess.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServiceB.DatabaseAccess;
using Shared.Model;

namespace ServiceB.Contacts.GetContact;

public sealed class EfGetContactClient : EfClient<ServiceBDbContext>, IGetContactClient
{
    public EfGetContactClient(ServiceBDbContext dbContext) : base(dbContext) { }

    public Task<Contact?> GetContactAsync(int id, CancellationToken cancellationToken = default) =>
        DbContext.Contacts.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
}
