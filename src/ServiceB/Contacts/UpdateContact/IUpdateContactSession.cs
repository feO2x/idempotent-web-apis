using System.Threading;
using System.Threading.Tasks;
using Light.SharedCore.DatabaseAccessAbstractions;
using Shared.Model;

namespace ServiceB.Contacts.UpdateContact;

public interface IUpdateContactSession : ISession
{
    Task<Contact?> GetContactAsync(int id, CancellationToken cancellationToken = default);
}
