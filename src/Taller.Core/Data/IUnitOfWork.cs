using System.Threading.Tasks;

namespace Taller.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
