using LogisticBot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticBot.DataAccess.Repositories.IRepository
{
    public  interface ICargoRepository
    {
        Task<long>  AddAsync(Cargo cargo);
        Task<long> UpdateAsync(Cargo cargo, long Id);
        Task DeleteAsync(long id);
        Task<Cargo> GetByIdAsync(long id);
        Task<List<Cargo>> GetAllAsync();
    }
}
