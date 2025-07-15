using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Contacts;

public interface IGetContactsClient : IAsyncDisposable
{
    Task<List<ContactListDto>> GetContactsAsync(
        int pageSize,
        Guid? lastKnownId = null,
        CancellationToken cancellationToken = default
    );
}