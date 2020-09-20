using Beltzac.Account.Domain;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beltzac.Account.Data
{
    public class AccountRepository : IRepository<Domain.Account>
    {
        private static ConcurrentDictionary<string, Domain.Account> _accounts = new ConcurrentDictionary<string, Domain.Account>();

        public void Add(Domain.Account entity)
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

        public Domain.Account Get(string id)
        {
            _accounts.TryGetValue(id, out var account);
            return account;
        }

        public IEnumerable<Domain.Account> GetAll()
        {
            return _accounts.Values;
        }
    }
}
