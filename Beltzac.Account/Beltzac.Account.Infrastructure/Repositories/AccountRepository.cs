using Beltzac.Account.Domain;
using Beltzac.Account.Domain.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beltzac.Account.Infrastructure.Repositories
{
    public class AccountRepository : IRepository<Domain.Models.Account>
    {
        private static ConcurrentDictionary<string, Domain.Models.Account> _accounts = new ConcurrentDictionary<string, Domain.Models.Account>();

        public void Add(Domain.Models.Account entity)
        {
            _accounts.TryAdd(entity.Id, entity);
        }

        public void Delete(string id)
        {
            _accounts.TryRemove(id, out var _);
        }

        public void DeleteAll()
        {
            _accounts.Clear();
        }

        public Domain.Models.Account Get(string id)
        {
            _accounts.TryGetValue(id, out var account);
            return account;
        }

        public IEnumerable<Domain.Models.Account> GetAll()
        {
            return _accounts.Values;
        }
    }
}
