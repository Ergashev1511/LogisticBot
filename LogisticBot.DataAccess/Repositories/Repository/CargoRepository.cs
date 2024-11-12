using LogisticBot.DataAccess.DBContext;
using LogisticBot.DataAccess.Repositories.IRepository;
using LogisticBot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticBot.DataAccess.Repositories.Repository
{
    public class CargoRepository : ICargoRepository
    {
        private readonly AppDbContext _dbContext;
        public CargoRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<long> AddAsync(Cargo cargo)
        {
            try
            {
                await _dbContext.Cargos.AddAsync(cargo);
                await _dbContext.SaveChangesAsync();
                return cargo.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public Task DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Cargo>> GetAllAsync()
        {
            try
            {
                return await _dbContext.Cargos.Include(a=>a.User).ToListAsync();    
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Cargo>();
            }
        }

        public Task<Cargo> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<long> UpdateAsync(Cargo cargo, long Id)
        {
            throw new NotImplementedException();
        }
    }
}
