using System.Threading.Tasks;
using Taller.Api.Entities;
using Taller.Core.Extensions;
using Taller.Core.Interfaces;

namespace Taller.Web.Services
{
    public interface ICarRepository : IRepository<Car>
    {
        Task<PagedResult<Car>> GetAllCarsPaged(int pageSize, int pageIndex, string query = null);
    }
}
