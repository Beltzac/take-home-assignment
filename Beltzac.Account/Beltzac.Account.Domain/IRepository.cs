using System;
using System.Collections.Generic;
using System.Text;

namespace Beltzac.Account.Domain
{
    public interface IRepository<T>
    {
        public void Add(T entity);
        public void Delete(int id);
        public void DeleteAll();
        public T Get(int id);
        public IEnumerable<T> GetAll();
    }
}
