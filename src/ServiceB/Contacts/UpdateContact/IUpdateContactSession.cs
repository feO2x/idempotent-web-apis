using System;
using System.Threading;
using System.Threading.Tasks;
using Light.SharedCore.DatabaseAccessAbstractions;
using Shared.Contacts;

namespace ServiceB.Contacts.UpdateContact;

public interface IUpdateContactSession : ISession
{
    Task<Contact?> GetContactAsync(Guid id, CancellationToken cancellationToken = default);
}
