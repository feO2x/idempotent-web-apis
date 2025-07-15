using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Shared.Model;

namespace Shared.Contacts;

public interface IGetContactsClient : IAsyncDisposable
{
    Task<List<ContactListDto>> GetContactsAsync(
        int pageSize,
        int? lastKnownId = null,
        CancellationToken cancellationToken = default
    );
}