using Beauty.Entity.Entities;

namespace Beauty.Repository.Contracts
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();

        Task<IEnumerable<UserRole>> GetUserRolesAsync();

        Task<IEnumerable<User>> GetUsersByRoleIdAsync(int roleId);

        Task<User> GetUserAsync(int userId);

        Task<User> GetUserByEmailAsync(string email);

        Task<User> GetUserByCredentialAsync(User login);

        Task CreateUserAsync(User user);

        void DeleteUser(User user);

        Task SaveAsync();
    }
}
