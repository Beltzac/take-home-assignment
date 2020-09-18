using Beltzac.Account.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beltzac.Account.Data
{
    public class AccountRepository : IRepository<Domain.Account>
    {
        private List<Domain.Account> _accounts = new List<Domain.Account>();

        public void Add(Domain.Account entity)
        {
            _accounts.Add(entity);
        }

        public void Delete(int id)
        {
            _accounts.RemoveAll(item => item.Id == id);
        }

        public Domain.Account Get(int id)
        {
            return _accounts.FirstOrDefault(item => item.Id == id);
        }

        public IEnumerable<Domain.Account> GetAll()
        {
            return _accounts;
        }
    }
}
