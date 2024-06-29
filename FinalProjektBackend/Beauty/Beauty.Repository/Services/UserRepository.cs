using Beauty.Entity.Entities;
using Beauty.Repository.Contracts;
using Beauty.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Beauty.Repository.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
        }

        public async Task<User> GetUserAsync(int userId)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Id.Equals(userId));
        }

        public async Task<User> GetUserByCredentialAsync(User login)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Email.Equals(login.Email)
                && x.Password.Equals(login.Password));
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .SingleOrDefaultAsync(x => x.Email.Equals(email));
        }

        public async Task<IEnumerable<User>> GetUsersByRoleIdAsync(int roleId)
        {
            return await _context.Users
                .Where(x => x.RoleId.Equals(roleId)).ToListAsync();
        }

        public async Task<IEnumerable<UserRole>> GetUserRolesAsync()
        {
            return await _context.UserRoles
                                 .Include(x => x.User)
                                 .Include(x => x.Role)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.Include(x => x.UserRoles)
                .ThenInclude(y => y.Role)
                .ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
