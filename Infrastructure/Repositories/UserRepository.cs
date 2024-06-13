using Microsoft.EntityFrameworkCore;
using DomainModule.Interfaces;
using DomainModule.Entities;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;
        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddUser(User user)
        {
            await _dbContext.Users.AddAsync(user);
            return (await _dbContext.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteUserById(int id)
        {
            User user = await GetUserById(id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                return (await _dbContext.SaveChangesAsync()) > 0;
            }
            return false;
        }

        public async Task<User> GetUserById(int id)
        {
             return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IReadOnlyList<User>> GetUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<bool> UpdateUser(User user)
        {
            var userToUpdate = await GetUserById(user.Id);
            if(userToUpdate != null){
                
                userToUpdate.Name = user.Name;
                userToUpdate.Email = user.Email;

                _dbContext.Users.Update(userToUpdate);
                return (await _dbContext.SaveChangesAsync()) > 0;
            }
            return false;
        }

    }
}