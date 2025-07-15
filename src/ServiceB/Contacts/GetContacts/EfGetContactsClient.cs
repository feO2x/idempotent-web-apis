using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Light.DatabaseAccess.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServiceB.DatabaseAccess;
using Shared.Contacts;

namespace ServiceB.Contacts.GetContacts;

public sealed class EfGetContactsClient : EfClient<ServiceBDbContext>, IGetContactsClient
{
    public EfGetContactsClient(ServiceBDbContext dbContext) : base(dbContext) { }

    public Task<List<ContactListDto>> GetContactsAsync(
        int pageSize,
        Guid? lastKnownId = null,
        CancellationToken cancellationToken = default
    )
    {
        IQueryable<Contact> query = DbContext.Contacts.OrderBy(x => x.Id);
        if (lastKnownId.HasValue)
        {
            query = query.Where(x => x.Id > lastKnownId.Value);
        }

        return query
           .Take(pageSize)
           .Select(x => new ContactListDto(x.Id, x.Name))
           .ToListAsync(cancellationToken);
    }
}