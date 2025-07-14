using System.Threading;
using System.Threading.Tasks;
using Light.DatabaseAccess.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServiceB.DatabaseAccess;
using Shared.Model;

namespace ServiceB.Contacts.DeleteContact;

public sealed class EfDeleteContactSession : EfSession<ServiceBDbContext>, IDeleteContactSession
{
    public EfDeleteContactSession(ServiceBDbContext dbContext) : base(dbContext) { }

    public Task<Contact?> GetContactAsync(int id, CancellationToken cancellationToken = default) =>
        DbContext.Contacts.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public void RemoveContact(Contact contact) => DbContext.Contacts.Remove(contact);
}
