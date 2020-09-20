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
        private static ConcurrentDictionary<int, Domain.Account> _accounts = new ConcurrentDictionary<int, Domain.Account>();

        public void Add(Domain.Account entity)
        {
            _accounts.TryAdd(entity.Id, entity);
        }

        public void Delete(int id)
        {
            _accounts.TryRemove(id, out var _);
        }

        public void DeleteAll()
        {
            _accounts.Clear();
        }

        public Domain.Account Get(int id)
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
