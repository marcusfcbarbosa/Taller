using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Taller.Core.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid id);
        void Add(T entity);
        Task Update(T entity);
    }
}
