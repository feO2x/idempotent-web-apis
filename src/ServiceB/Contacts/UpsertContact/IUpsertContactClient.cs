using System;
using System.Threading;
using System.Threading.Tasks;
using Shared.Contacts;

namespace ServiceB.Contacts.UpsertContact;

public interface IUpsertContactClient : IAsyncDisposable
{
    Task<UpsertResult> UpsertContactAsync(Contact contact, CancellationToken cancellationToken = default);
}
