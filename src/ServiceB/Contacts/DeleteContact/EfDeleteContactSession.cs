using System;
using System.Threading;
using System.Threading.Tasks;
using Light.DatabaseAccess.EntityFrameworkCore;
using ServiceB.Contacts.Shared;
using ServiceB.DatabaseAccess;
using Shared.Contacts;

namespace ServiceB.Contacts.DeleteContact;

public sealed class EfDeleteContactSession : EfSession<ServiceBDbContext>, IDeleteContactSession
{
    public EfDeleteContactSession(ServiceBDbContext dbContext) : base(dbContext) { }

    public Task<Contact?> GetContactAsync(Guid id, CancellationToken cancellationToken = default) =>
        DbContext.GetContactAsync(id, cancellationToken);

    public void RemoveContact(Contact contact) => DbContext.Contacts.Remove(contact);
}
