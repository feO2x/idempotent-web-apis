using System;
using System.Threading;
using System.Threading.Tasks;
using Shared.Contacts;

namespace ServiceB.Contacts.CreateContact;

public interface ICreateContactClient : IAsyncDisposable
{
    Task<UpsertResult> UpsertContactAsync(Contact contact, CancellationToken cancellationToken = default);
}
