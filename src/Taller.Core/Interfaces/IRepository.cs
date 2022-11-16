using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taller.Core.Data;

namespace Taller.Core.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid id);
        void Add(T entity);
        Task Update(T entity);

        Task DeleteById(Guid id);
    }
}
