using Beauty.Entity.Entities;
using Beauty.Repository.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Repository
{
    public class EmployeeCalendarRepositoryTest
    {
        private EmployeeCalendarRepository empC_Repo;


        [Fact]
        public async Task EmployeeCalendarRepository_CreateEmployeeCalendarAsync_GetEmployeeCalendarAsync_DeleteEmployeeCalendar() 
        {
            //Arrange
            var employeeC = new EmployeeCalendar
            {
                Id=978,
                Date = "13.06.2024",
                BookTime = 30,
                IsVacation=true,
                VacationDescription="Urlaub",
                Employee = new Employee { User = new User { FirstName = "Max", LastName = "Müller", Email = "müller123@gmail.com", Telephone = "+123456", Password = "password123", RoleId = 3 } },
            };
            var _db = await Context.GenerateContext();
            empC_Repo = new EmployeeCalendarRepository(_db);
            //Act für Creat & Get()
            await empC_Repo.CreateEmployeeCalendarAsync(employeeC);
            await empC_Repo.SaveAsync();
            var empc=await empC_Repo.GetEmployeeCalendarAsync(978);
            //Assert
            Assert.NotNull(empc);
            Assert.IsType<EmployeeCalendar>(empc);
            Assert.Equal(empc.Id, 978);

            //Act für Delete & Get
            empC_Repo.DeleteEmployeeCalendar(employeeC);
            await empC_Repo.SaveAsync();
            var empcNull = await empC_Repo.GetEmployeeCalendarAsync(978);
            //Assert
            Assert.Null(empcNull);
        }

        [Fact]
        public async Task EmployeeCalendarRepository_GetEmployeeCalendarsAsync()
        {
            //Arrange
            var _db = await Context.Seed();
            empC_Repo=new EmployeeCalendarRepository(_db);
            //Act
            var empCalendars= await empC_Repo.GetEmployeeCalendarsAsync();
            //Assert
            Assert.NotNull(empCalendars);  
            Assert.IsType<List<EmployeeCalendar>>(empCalendars);
        }

        [Fact]
        public async Task EmployeeCalendarRepository_GetEmployeeCalendarsByEmployeeIdAsync() //(int id)
        {
            //Arrange
            var _db = await Context.Seed();
            empC_Repo = new EmployeeCalendarRepository(_db);
            var employeeC = new EmployeeCalendar
            {
                Id = 978,
                Date = "13.06.2024",
                BookTime = 30,
                IsVacation = true,
                VacationDescription = "Urlaub",
                Employee = new Employee { Id= 789, User = new User { FirstName = "Max", LastName = "Müller", Email = "müller123@gmail.com", Telephone = "+12345", Password = "password123", RoleId = 3 } },
            };

            //Act
            await empC_Repo.CreateEmployeeCalendarAsync(employeeC);
            await empC_Repo.SaveAsync();
            var empCalendars = await empC_Repo.GetEmployeeCalendarsByEmployeeIdAsync(789);
            //Assert
            Assert.NotNull(empCalendars);
            Assert.IsType<List<EmployeeCalendar>>(empCalendars);

        }


        [Fact]
        public async Task EmployeeCalendarRepository_GetEmployeeCalendarAsync_ShouldPreventSQLInjection()  //(int userId)
        {
            //Arrange
            var _db = await Context.Seed();
            empC_Repo = new EmployeeCalendarRepository(_db);
            var maliciousInput = "1; DROP TABLE EmployeeCalendar; --";
            //Act & assert
            await Assert.ThrowsAsync<FormatException>(() => empC_Repo.GetEmployeeCalendarAsync(int.Parse(maliciousInput)));
        }


        [Fact]
        public async Task EmployeeCalendarRepository_GetEmployeeCalendarsByEmployeeIdAsync_ShouldPreventSQLInjection() //(int id)
        {
            //Arrange
            var _db = await Context.Seed();
            empC_Repo = new EmployeeCalendarRepository(_db);
            var maliciousInput = "1; DROP TABLE EmployeeCalendar; --";
            //Act & assert
            await Assert.ThrowsAsync<FormatException>(() => empC_Repo.GetEmployeeCalendarsByEmployeeIdAsync(int.Parse(maliciousInput)));
        }







    }
}
