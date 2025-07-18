using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ServiceA.Contacts.DeleteContact;

public interface IDeleteContactClient : IAsyncDisposable
{
    Task<IResult> DeleteContactAsync(Guid id, CancellationToken cancellationToken = default);
}
