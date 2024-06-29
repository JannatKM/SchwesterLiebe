using Beauty.Entity.Entities;
using Beauty.Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Repository
{
    public class EmployeeRepositoryTest
    {
        private EmployeeRepository _empRepo;


        [Fact]
        public async Task EmployeeRepository_DeleteEmployee_GetEmployeeAsync_CreateEmployeeAsync()   
        {
            //Arrange
            var _db = await Context.GenerateContext();
            _empRepo = new EmployeeRepository(_db);
            var id = 431;
            var emp = new Employee
            {
                Id = id,
                User = new User
                {
                   
                    FirstName = "Max",
                    LastName = "Müller",
                    Email = "müller123@gmail.com",
                    Password = "password123",
                    Telephone = "+0612345",
                    RoleId = 3
                }
            };
            //Act
            await _empRepo.CreateEmployeeAsync(emp);
            await _empRepo.SaveAsync();
            var employee = await _empRepo.GetEmployeeAsync(id);
            //Assert für Create & Get
            Assert.NotNull(employee);
            Assert.IsType<Employee>(employee);
            Assert.Equal(employee.Id, id);
            //Act
            _empRepo.DeleteEmployee(emp);
            await _empRepo.SaveAsync();
            var employeeNull = await _empRepo.GetEmployeeAsync(id);
            //Assert für Delete & Get
            Assert.Null(employeeNull);
           
        }

        [Fact]
        public async Task EmployeeRepository_GetEmployeeByUserIdAsync()   //(int employeeId)
        {
            //Arrange
            var _db = await Context.GenerateContext();
            _empRepo = new EmployeeRepository(_db);
            var id = 431;
            var emp = new Employee
            {
                User = new User
                {
                    Id = id,
                    FirstName = "Max",
                    LastName = "Müller",
                    Email = "müller123@gmail.com",
                    Password = "password123",
                    Telephone = "+0612345",
                    RoleId = 3
                }
            };

            //Act
            await _empRepo.CreateEmployeeAsync(emp);
            await _empRepo.SaveAsync();
            var employee= await _empRepo.GetEmployeeByUserIdAsync(id);
            //Assert
            Assert.NotNull(employee );
            Assert.IsType<Employee>(employee);
            Assert.Equal(employee.User.Id, id);

        }


        [Fact]
        public async Task EmployeeRepository_GetEmployeesAsync()
        {
            //Arrange
            var _db = await Context.Seed();
            _empRepo = new EmployeeRepository(_db);
            //Act
           var employees= await _empRepo.GetEmployeesAsync();
            //Assert
            Assert.NotNull(employees);
            Assert.IsType<List<Employee>>(employees);

        }


        [Fact]
        public async Task EmployeeRepository_GetEmployeeByUserIdAsync_ShouldPreventSQLInjection()   //(int employeeId)
        {
            //Arrange
            var _db = await Context.GenerateContext();
            _empRepo = new EmployeeRepository(_db);
            var maliciousInput = "1; DROP TABLE Employee; --";
            //Act & assert
            await Assert.ThrowsAsync<FormatException>(() => _empRepo.GetEmployeeByUserIdAsync(int.Parse(maliciousInput)));
        }


        [Fact]
        public async Task EmployeeRepository_GetEmployeeAsync_ShouldPreventSQLInjection()   //(int employeeId)
        {
            //Arrange
            var _db = await Context.GenerateContext();
            _empRepo = new EmployeeRepository(_db);
            var maliciousInput = "1; DROP TABLE Employee; --";
            //Act & assert
            await Assert.ThrowsAsync<FormatException>(() => _empRepo.GetEmployeeAsync(int.Parse(maliciousInput)));
        }




    }
}
