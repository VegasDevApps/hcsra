using DomainModule.Entities;

namespace DomainModule.Interfaces
{
    public interface IUserRepository
    {
        Task<IReadOnlyList<User>> GetUsers();
        Task<User> GetUserById(int id);
        Task<bool> AddUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUserById(int id);
    }
}