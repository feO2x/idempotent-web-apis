using System;
using System.Threading;
using System.Threading.Tasks;
using Shared.Model;

namespace Shared.Contacts;

public interface IGetContactClient : IAsyncDisposable
{
    Task<Contact?> GetContactAsync(int id, CancellationToken cancellationToken = default);
}
