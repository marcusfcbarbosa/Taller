using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taller.Api.Data;
using Taller.Api.Entities;
using Taller.Core.Extensions;
using Taller.Core.Interfaces;

namespace Taller.Api.Repositories
{
    public interface ICarRepository : IRepository<Car>
    {
        Task<PagedResult<Car>> GetAllCarsPaged(int pageSize, int pageIndex, string query = null);
    }
    public class CarRepository : ICarRepository
    {
        private readonly TallerDBContext _context;
        public CarRepository(TallerDBContext context)
        {
            _context = context;
        }
        public async Task<PagedResult<Car>> GetAllCarsPaged(int pageSize, int pageIndex, string query = null)
        {
            var sql = @$"SELECT * FROM Cars 
                      WHERE (@Nome IS NULL OR Model LIKE '%' + @Nome + '%') 
                      ORDER BY [Model] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS 
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(Id) FROM Cars 
                      WHERE (@Nome IS NULL OR Model LIKE '%' + @Nome + '%')";

            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });

            var produtos = multi.Read<Car>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedResult<Car>()
            {
                List = produtos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query
            };
        }
        public void Add(Car entity)
        {
            _context.Cars.Add(entity);
        }

        public async Task Update(Car entity)
        {
            _context.Cars.Update(entity);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
        public async Task<Car> GetById(Guid id)
        {
            return await _context.Cars.FindAsync(id);
        }
        public async Task<IEnumerable<Car>> GetAll()
        {
            return await _context.Cars.AsNoTracking().ToListAsync();
        }
    }
}

