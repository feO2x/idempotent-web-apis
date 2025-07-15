using System;
using System.Threading;
using System.Threading.Tasks;
using Shared.Contacts;

namespace ServiceA.Contacts.CreateContact;

public interface ICreateContactClient : IAsyncDisposable
{
    Task<Contact> CreateContactAsync(CreateContactDto dto, CancellationToken cancellationToken = default);
}
