using Beauty.Entity.Entities;
using Beauty.Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Repository
{
    public class UserRepositoryTest
    {
        private UserRepository _userRepo;

        [Fact]
        public async Task UserRepository_CreateUserAsync_DeleteUser_GetUserAsync()
        {
            //Arrange
            var _db = await Context.GenerateContext();
            _userRepo=new UserRepository(_db);
            var id = 555;
            var user = new User
            {
                Id = id,
                FirstName = "Max",
                LastName = "Müller",
                Email = "müller123@gmail.com",
                Telephone = "+12345",
                Password = "password123",
                RoleId = 3
            };
            //Act
            await _userRepo.CreateUserAsync(user);
            await _userRepo.SaveAsync();
            var testuser= await _userRepo.GetUserAsync(id);
            //Assert Create & Get
            Assert.NotNull(testuser);
            Assert.Equal(id, testuser.Id);

            //Act
            _userRepo.DeleteUser(user);
            await _userRepo.SaveAsync();
            var testuserNull = await _userRepo.GetUserAsync(id);
            //Assert 
            Assert.Null(testuserNull);
 
        }

        [Fact]
        public async Task UserRepository_GetUserByCredentialAsync()  //(User login)
        {  
            //Arrange
            var _db = await Context.GenerateContext();
            _userRepo = new UserRepository(_db);
            var id = 555;
            var user = new User
            {
                Id = id,
                FirstName = "Max",
                LastName = "Müller",
                Email = "müller123@gmail.com",
                Telephone = "+12345",
                Password = "password123",
                RoleId = 3
            };
            //Act
            await _userRepo.CreateUserAsync(user);
            await _userRepo.SaveAsync();
            var testuser = await _userRepo.GetUserByCredentialAsync(user);
            //Assert 
            Assert.NotNull(testuser);
            Assert.Equal(testuser.Id, user.Id);

        }

        [Fact]
        public async Task UserRepository_GetUserByEmailAsync() //(string email)
        {
            //Arrange
            var _db = await Context.GenerateContext();
            _userRepo = new UserRepository(_db);
            var id = 555;
            var email = "müller123@gmail.com";
            var user = new User
            {
                Id = id,
                FirstName = "Max",
                LastName = "Müller",
                Email = email,
                Telephone = "+12345",
                Password = "password123",
                RoleId = 3
            };
            //Act
            await _userRepo.CreateUserAsync(user);
            await _userRepo.SaveAsync();
            var testuser = await _userRepo.GetUserByEmailAsync(email);
            //Assert
            Assert.NotNull(testuser);
            Assert.Equal(email, testuser.Email);
        }

        [Fact]
        public async Task UserRepository_GetUsersByRoleIdAsync()  //(int roleId)
        {
            //Arrange
            var _db = await Context.GenerateContext();
            _userRepo = new UserRepository(_db);
            var id = 555;
            var roleId = 3;
            var user = new User
            {
                Id = id,
                FirstName = "Max",
                LastName = "Müller",
                Email = "müller123@gmail.com",
                Telephone = "+12345",
                Password = "password123",
                RoleId = roleId
            };
            //Act
            await _userRepo.CreateUserAsync(user);
            await _userRepo.SaveAsync();
            var testusers = await _userRepo.GetUsersByRoleIdAsync(roleId);
            //Assert 
            Assert.NotNull(testusers);
            Assert.True(testusers.Count() > 0);
        }

        [Fact]
        public async Task UserRepository_GetUserRolesAsync()
        {
            //Arrange
            var _db = await Context.Seed();
            _userRepo = new UserRepository(_db);
            //Act
            var userRoles= await _userRepo.GetUserRolesAsync();
            //Assert 
            Assert.NotNull(userRoles);

        }


        [Fact]
        public async Task UserRepository_GetUsersAsync()
        {
            //Arrange
            var _db = await Context.Seed();
            _userRepo = new UserRepository(_db);
            //Act
            var users =  await _userRepo.GetUsersAsync();
            //Assert 
            Assert.NotNull(users);
            Assert.IsType<List<User>>(users);


        }



        [Fact]
        public async Task UserRepository_GetUserAsync_ShouldPreventSQLInjection()
        {

            //Arrange
            var _db = await Context.Seed();
            _userRepo = new UserRepository(_db);
            var maliciousInput = "1; DROP TABLE Employee; --";
            //Act & assert
            await Assert.ThrowsAsync<FormatException>(() => _userRepo.GetUserAsync(int.Parse(maliciousInput)));
        }



        [Fact]
        public async Task UserRepository_GetUserByEmailAsync_ShouldPreventSQLInjection() //(string email)
        {
            //Arrange
            var _db = await Context.Seed();
            _userRepo = new UserRepository(_db);
            var maliciousInput = "müller123@gmail.com; DROP TABLE Employee; --";
            //Act
            var testuser = await _userRepo.GetUserByEmailAsync(maliciousInput);
            //Assert
            Assert.Null(testuser);
        }


        [Fact]
        public async Task UserRepository_GetUsersByRoleIdAsync__ShouldPreventSQLInjection()  //(int roleId)
        {
            //Arrange
            var _db = await Context.Seed();
            _userRepo = new UserRepository(_db);
            var maliciousInput = "1; DROP TABLE Employee; --";
            //Act & Act
            await Assert.ThrowsAsync<FormatException>(() => _userRepo.GetUsersByRoleIdAsync(int.Parse(maliciousInput)));
        }

    }
}
