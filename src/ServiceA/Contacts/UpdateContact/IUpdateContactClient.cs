using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Shared.Contacts;

namespace ServiceA.Contacts.UpdateContact;

public interface IUpdateContactClient : IAsyncDisposable
{
    Task<IResult> UpdateContactAsync(Contact contact, CancellationToken cancellationToken = default);
}
