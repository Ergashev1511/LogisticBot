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
    public class UserRepository : IUserRepository
    {

        private readonly AppDbContext _dbcontext;
        public UserRepository(AppDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        public async Task<long> AddAsync(User user)
        {
            try
            {
                await _dbcontext.Users.AddAsync(user);
                await _dbcontext.SaveChangesAsync();

                return user.Id;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public async Task<long> DeleteAsync(long id)
        {
            try
            {
                var user=await _dbcontext.Users.FindAsync(id);
                
               _dbcontext.Users.Remove(user);  
                await _dbcontext.SaveChangesAsync();    
                return user.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public async Task<List<User>> GetAllAsync()
        {
            try
            {
                return await _dbcontext.Users.Include(a=>a.Cargos).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<User>();
            }
        }

        public async Task<User> GetByIdAsync(long id)
        {
            try
            {
                return await _dbcontext.Users.Include(a => a.Cargos).FirstOrDefaultAsync(a => a.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new User();
            }
        }

        public Task<long> UpdateAsync(User user, long Id)
        {
            throw new NotImplementedException();
        }
    }
}
