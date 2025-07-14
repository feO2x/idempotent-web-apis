using System.Threading;
using System.Threading.Tasks;
using Light.SharedCore.DatabaseAccessAbstractions;
using Shared.Model;

namespace ServiceB.Contacts.DeleteContact;

public interface IDeleteContactSession : ISession
{
    Task<Contact?> GetContactAsync(int id, CancellationToken cancellationToken = default);
    void RemoveContact(Contact contact);
}
