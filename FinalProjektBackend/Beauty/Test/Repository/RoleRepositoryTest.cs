using Beauty.Entity.Entities;
using Beauty.Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Repository
{
    public class RoleRepositoryTest
    {
        private RoleRepository _roleRepo;

        [Fact]
        public async Task RoleRepository_CreateRoleAsync_DeleteRole_GetRoleAsync()
        {
            //Arrange
            var id = 222;
            var role = new Role
            {
                Id=id,
                Name = "Admin"
            };
            var _db = await Context.GenerateContext();
            _roleRepo = new RoleRepository(_db);
            //Act
            await _roleRepo.CreateRoleAsync(role);
            await _roleRepo.SaveAsync();
            var r = await _roleRepo.GetRoleAsync(id);
            //Assert Create & Get
            Assert.NotNull(r);
            Assert.Equal(id, r.Id);

            //Act
            _roleRepo.DeleteRole(role);
            await _roleRepo.SaveAsync();
            var roleNull = await _roleRepo.GetRoleAsync(id);
            //Assert Delete & Get
            Assert.Null(roleNull);
        }


        [Fact]
        public async Task RoleRepository_CreateUserRoleAsync()
        {
            //Arrange
          
            var userRole = new UserRole
            {
                User= new User { FirstName = "Max", LastName = "Müller", Email = "müller123@gmail.com", Telephone = "+12345", Password = "password123", RoleId = 3 },
                Role=new Role { Name="Admin"}
            };
            var _db = await Context.GenerateContext();
            _roleRepo = new RoleRepository(_db);

            //Act
            await _roleRepo.CreateUserRoleAsync(userRole);
            await _roleRepo.SaveAsync();
            //Assert Create & Get
            Assert.True(_db.UserRoles.Count() > 0);
        }


        [Fact]
        public async Task RoleRepository_GetRolesAsync()
        {
            //Arrange
            var _db = await Context.Seed();
            _roleRepo = new RoleRepository(_db);
            //Act
            var roles=await _roleRepo.GetRolesAsync();
            //Assert
            Assert.NotNull(roles);
            Assert.IsType<List<Role>>(roles);
        }

        [Fact]
        public async Task RoleRepository_GetRoleAsync_ShouldPreventSQLInjection()
        {
            //Arrange
            var _db = await Context.Seed();
            _roleRepo = new RoleRepository(_db);
            var maliciousInput = "1; DROP TABLE Employee; --";
            //Act & assert
            await Assert.ThrowsAsync<FormatException>(() => _roleRepo.GetRoleAsync(int.Parse(maliciousInput)));
        }















    }
}
