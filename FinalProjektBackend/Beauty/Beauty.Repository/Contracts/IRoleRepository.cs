using Beauty.Entity.Entities;

namespace Beauty.Repository.Contracts
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetRolesAsync();

        Task<Role> GetRoleAsync(int roleId);

        Task CreateRoleAsync(Role role);

        Task CreateUserRoleAsync(UserRole userRole);

        void DeleteRole(Role role);

        Task SaveAsync();
    }
}
