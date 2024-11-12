using LogisticBot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticBot.DataAccess.Repositories.IRepository
{
    public interface IUserRepository
    {
        Task<long>  AddAsync(User user);
        Task<long> UpdateAsync(User user, long Id);
        Task<long> DeleteAsync(long id);

        Task<User> GetByIdAsync(long id);
        Task<List<User>> GetAllAsync();

    }
}
