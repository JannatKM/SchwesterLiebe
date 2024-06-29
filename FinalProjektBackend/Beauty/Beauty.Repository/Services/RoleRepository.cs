using Beauty.Entity.Entities;
using Beauty.Repository.Contracts;
using Beauty.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace Beauty.Repository.Services
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateRoleAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
        }

        public async Task CreateUserRoleAsync(UserRole userRole)
        {
            await _context.UserRoles.AddAsync(userRole);
        }

        public void DeleteRole(Role role)
        {
            _context.Roles.Remove(role);
        }

        public async Task<Role> GetRoleAsync(int roleId)
        {
            return await _context.Roles
                .SingleOrDefaultAsync(x => x.Id.Equals(roleId));
        }

        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
