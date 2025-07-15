using Light.DatabaseAccess.EntityFrameworkCore;
using ServiceB.DatabaseAccess;
using Shared.Contacts;

namespace ServiceB.Contacts.CreateContact;

public sealed class EfCreateContactSession : EfSession<ServiceBDbContext>, ICreateContactSession
{
    public EfCreateContactSession(ServiceBDbContext dbContext) : base(dbContext) { }
    public void AddContact(Contact contact) => DbContext.Contacts.Add(contact);
}
