using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceA.Contacts.DeleteContact;

public interface IDeleteContactClient : IAsyncDisposable
{
    Task DeleteContactAsync(int id, CancellationToken cancellationToken = default);
}
