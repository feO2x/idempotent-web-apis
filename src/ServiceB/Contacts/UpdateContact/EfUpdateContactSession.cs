using System;
using System.Threading;
using System.Threading.Tasks;
using Light.DatabaseAccess.EntityFrameworkCore;
using ServiceB.Contacts.Shared;
using ServiceB.DatabaseAccess;
using Shared.Contacts;

namespace ServiceB.Contacts.UpdateContact;

public sealed class EfUpdateContactSession : EfSession<ServiceBDbContext>, IUpdateContactSession
{
    public EfUpdateContactSession(ServiceBDbContext dbContext) : base(dbContext) { }

    public Task<Contact?> GetContactAsync(Guid id, CancellationToken cancellationToken = default) =>
        DbContext.GetContactAsync(id, cancellationToken);
}
