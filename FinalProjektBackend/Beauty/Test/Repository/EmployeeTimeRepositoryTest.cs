using Beauty.Entity.Entities;
using Beauty.Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Repository
{
    public class EmployeeTimeRepositoryTest
    {
        private EmployeeTimeRepository _employeeTimeRepo;

        [Fact]
        public async Task EmployeeTimeRepo_CreateEmployeeTimeAsync_DeleteEmployeeTime_GetEmployeeTimeByIdAsync()
        {
            //Arrange
              var id = 123;
              var _db = await Context.GenerateContext();
            _employeeTimeRepo = new EmployeeTimeRepository(_db);
            var employeeTime = new EmployeeTime()
            {
                Id=id,
                Date = "13.06.2024",
                Time = "13:00",
                IsReserved = true,
                Employee = new Employee { User = new User { FirstName = "Max", LastName = "Müller", Email = "müller123@gmail.com", Telephone = "+123456", Password = "password123", RoleId = 3 } }
            };
            //Act 
            await _employeeTimeRepo.CreateEmployeeTimeAsync(employeeTime);
            await _employeeTimeRepo.SaveAsync();
            var empTime= await _employeeTimeRepo.GetEmployeeTimeByIdAsync(id);
            //Assert Create & Get
            Assert.NotNull(empTime);
            Assert.IsType<EmployeeTime>(empTime);
            Assert.Equal(id, empTime.Id);

            //Act 
            _employeeTimeRepo.DeleteEmployeeTime(employeeTime);
            await _employeeTimeRepo.SaveAsync();
            var empTimeNull = await _employeeTimeRepo.GetEmployeeTimeByIdAsync(id);
            //Assert Delete & Get
            Assert.Null(empTimeNull);
 
        }

        [Fact]
        public async Task EmployeeTimeRepo_GetEmployeeTimesByDateAsync()
        {
            //Arrange
            var _db = await Context.GenerateContext();
            _employeeTimeRepo = new EmployeeTimeRepository(_db);
            var employeeTime = new EmployeeTime()
            {
                Date = "13.06.2024",
                Time = "13:00",
                IsReserved = true,
                Employee = new Employee { User = new User { FirstName = "Max", LastName = "Müller", Email = "müller123@gmail.com", Telephone = "+12345", Password = "password123", RoleId = 3 } }
            };
            //Act
            await _employeeTimeRepo.CreateEmployeeTimeAsync(employeeTime);
            await _employeeTimeRepo.SaveAsync();
            var empTimes = await _employeeTimeRepo.GetEmployeeTimesByDateAsync("13.06.2024");
            //Assert 
            Assert.NotNull(empTimes);
            Assert.IsType<List<EmployeeTime>>(empTimes);
            Assert.True(empTimes.Count() > 0);
        }
        [Fact]
        public async Task EmployeeTimeRepo_GetEmployeeTimesAsync()
        {
            //Arrange
            var _db = await Context.Seed();
            _employeeTimeRepo = new EmployeeTimeRepository(_db);
            //Act
            var empTimes = await _employeeTimeRepo.GetEmployeeTimesAsync();
            //Assert 
            Assert.NotNull(empTimes);
            Assert.IsType<List<EmployeeTime>>(empTimes);
        }

        [Fact]
        public async Task EmployeeTimeRepo_GetEmployeeTimesByDate2Async()// (int id, string date)
        {
            //Arrange
            var id = 123;
            var _db = await Context.GenerateContext();
            _employeeTimeRepo = new EmployeeTimeRepository(_db);
            var employeeTime = new EmployeeTime()
            {
                Date = "13.06.2024",
                Time = "13:00",
                IsReserved = true,
                Employee = new Employee {Id=id, User = new User { FirstName = "Max", LastName = "Müller", Email = "müller123@gmail.com", Telephone = "+12345", Password = "password123", RoleId = 3 } }
            };
            //Act
            await _employeeTimeRepo.CreateEmployeeTimeAsync(employeeTime);
            await _employeeTimeRepo.SaveAsync();
            var empTimes = await _employeeTimeRepo.GetEmployeeTimesByDateAsync(id,"13.06.2024");
           
            //Assert 
            Assert.NotNull(empTimes);
            Assert.IsType<List<EmployeeTime>>(empTimes);
            Assert.True(empTimes.Count() > 0);
        }

        [Fact]
        public async Task EmployeeTimeRepo_GetEmployeeTimesByEmployeeIdAsync()   //(int id)
        {
            //Arrange
            var id = 123;
            var _db = await Context.GenerateContext();
            _employeeTimeRepo = new EmployeeTimeRepository(_db);
            var employeeTime = new EmployeeTime()
            {
                Date = "13.06.2024",
                Time = "13:00",
                IsReserved = true,
                Employee = new Employee { Id = id, User = new User { FirstName = "Max", LastName = "Müller", Telephone = "+12345", Email = "müller123@gmail.com", Password = "password123", RoleId = 3 } }
            };
            //Act
            await _employeeTimeRepo.CreateEmployeeTimeAsync(employeeTime);
            await _employeeTimeRepo.SaveAsync();
            var empTimes = await _employeeTimeRepo.GetEmployeeTimesByEmployeeIdAsync(id);
            
            //Assert 
            Assert.NotNull(empTimes);
            Assert.IsType<List<EmployeeTime>>(empTimes);
            Assert.True(empTimes.Count() > 0);

        }


        [Fact]
        public async Task EmployeeTimeRepo_GetEmployeeTimeByIdAsync_ShouldPreventSQLInjection()
        {
            //Arrange
            var _db = await Context.Seed();
            _employeeTimeRepo = new EmployeeTimeRepository(_db);
            var maliciousInput = "1; DROP TABLE Employee; --";
            //Act & assert
            await Assert.ThrowsAsync<FormatException>(() => _employeeTimeRepo.GetEmployeeTimeByIdAsync(int.Parse(maliciousInput)));
        }


        [Fact]
        public async Task EmployeeTimeRepo_GetEmployeeTimesByDateAsync_ShouldPreventSQLInjection()
        {
            //Arrange
            var _db = await Context.Seed();
            _employeeTimeRepo = new EmployeeTimeRepository(_db);
            var maliciousInput = "13.06.2024'; DROP TABLE EmployeeTimes; --";
            //Act
            var empTimes = await _employeeTimeRepo.GetEmployeeTimesByDateAsync(maliciousInput);
            //Assert & Act
            Assert.Empty(empTimes);
        }



        [Fact]
        public async Task EmployeeTimeRepo_GetEmployeeTimesByDate2Async_ShouldPreventSQLInjection()// (int id, string date)
        {
            //Arrange
            var _db = await Context.Seed();
            _employeeTimeRepo = new EmployeeTimeRepository(_db);
            var maliciousInput = "13.06.2024'; DROP TABLE EmployeeTimes; --";
            //Act
            var empTimes = await _employeeTimeRepo.GetEmployeeTimesByDateAsync(1,maliciousInput);

            //Assert
            Assert.Empty(empTimes);
        }


        [Fact]
        public async Task EmployeeTimeRepo_GetEmployeeTimesByEmployeeIdAsync_ShouldPreventSQLInjection()   //(int id)
        {
            //Arrange
            var _db = await Context.Seed();
            _employeeTimeRepo = new EmployeeTimeRepository(_db);
            var maliciousInput = "1; DROP TABLE Employee; --";
            //Act & assert
            await Assert.ThrowsAsync<FormatException>(() => _employeeTimeRepo.GetEmployeeTimesByEmployeeIdAsync(int.Parse(maliciousInput)));
        }










    }
}
