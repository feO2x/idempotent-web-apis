using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Contacts;

public interface IGetContactClient : IAsyncDisposable
{
    Task<Contact?> GetContactAsync(Guid id, CancellationToken cancellationToken = default);
}
