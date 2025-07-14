using System.Threading;
using System.Threading.Tasks;
using Shared.Model;

namespace ServiceB.Contacts.GetContact;

public interface IGetContactClient
{
    Task<Contact?> GetContactAsync(int id, CancellationToken cancellationToken = default);
}
