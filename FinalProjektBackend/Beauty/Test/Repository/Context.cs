using Beauty.Entity.Entities;
using Beauty.Repository.Data;
using Bogus;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Repository
{
    public static class Context
    {


        public static async Task<ApplicationDbContext> GenerateContext()
        {
            var options = new DbContextOptionsBuilder()
                    .UseSqlite("Datasource=:memory:") 
                    .Options;

            var databaseContext = new ApplicationDbContext(options);
            
            databaseContext.Database.OpenConnection();
            databaseContext.Database.EnsureDeleted();
            databaseContext.Database.EnsureCreated();

            return databaseContext;

        }

        public static async Task<ApplicationDbContext> Seed()
        {
            Randomizer.Seed = new Random(1938);

            var databaseContext = await GenerateContext();

          
            var appoTypes = new Faker<AppointmentType>("de").CustomInstantiator(f =>
            {
                return new AppointmentType
                {
                    Type = "Laser"
                };
            }).Generate(10).ToList();
            databaseContext.AppointmentTypes.AddRange(appoTypes);
            databaseContext.SaveChanges();

            var discounts = new Faker<Discount>("de").CustomInstantiator(f =>
            {
                return new Discount
                {
                    EndDate = f.Date.Between(new DateTime(2024, 8, 30), new DateTime(2025, 1, 16)).ToString(),
                    Percent = f.Random.Int(5, 60),
                    StartDate = f.Date.Between(new DateTime(2024, 5, 30), new DateTime(2024, 8, 10)).ToString()
                };
            }).Generate(10).ToList();
            databaseContext.Discounts.AddRange(discounts);
            databaseContext.SaveChanges();

            var rooms = new Faker<Room>("de").CustomInstantiator(f =>
            {

                return new Room
                {
                    Name = f.PickRandom("Raum105", "Raum100", "Raum100"),
                    IsDown = f.Random.Bool(0.00f),
                   // Description = f.PickRandom("kaputt")
                };
            }).Generate(10).ToList();
            databaseContext.Rooms.AddRange(rooms);
            databaseContext.SaveChanges();

            var products = new Faker<Product>("de").CustomInstantiator(f => {
                return new Product
                {
                    Name = f.PickRandom("Laser"),
                    Description = f.PickRandom("Lorem ipsum"),
                    Duration = f.Random.Int(20,120),
                    Price = f.Random.Int(40, 200)
                };
            }).Generate(10).ToList();
            databaseContext.Products.AddRange(products);
            databaseContext.SaveChanges();

            var users = new Faker<User>("de").CustomInstantiator(f => {
                return new User
                {
                    FirstName = f.Name.FirstName(),
                    LastName = f.Name.LastName(),
                    Email = f.Internet.Email().ToString(),
                    Telephone = f.Phone.PhoneNumber(),
                    Password = f.PickRandom("1234567asdf"),
                    

                };
            }).Generate(20).ToList();
            databaseContext.Users.AddRange(users);
            databaseContext.SaveChanges();

            var employees = new Faker<Employee>("de").CustomInstantiator(f =>
            {
                return new Employee
                {
                    User = f.Random.ListItem(users), 
                };
            }).Generate(10).//GroupBy(e => new { e.UserId}).Select(g => g.First()). //Es bekommt gleiche UserIds, daher group by
            ToList();
            databaseContext.Employees.AddRange(employees);
            databaseContext.SaveChanges();


            var appos = new Faker<Appointment>("de").CustomInstantiator(f =>
            {

                return new Appointment
                {
                    IsSelected = f.Random.Bool(0.00f),
                    Date = f.Date.ToString(),
                    StartTime = f.PickRandom("9:00", "10:00", "11:00"),
                    EndTime = f.PickRandom("11:30", "12:00"),
                    Room = f.Random.ListItem(rooms),
                    AppointmentType = f.Random.ListItem(appoTypes),
                    Product = f.Random.ListItem(products),
                    Employee = f.Random.ListItem(employees),
                    Discount = f.Random.ListItem(discounts)
                };

            }).Generate(10).ToList();

            databaseContext.Appointments.AddRange(appos);
            databaseContext.SaveChanges();
             

            var userRoles = new Faker<UserRole>("de").CustomInstantiator(f =>
            {
                return new UserRole
                {
                    User = f.Random.ListItem(users),
                    Role = f.Random.ListItem(databaseContext.Roles.ToList()) 
                };
            }).Generate(20).
            GroupBy(ur => new { ur.UserId }).Select(g => g.First()) 
            .ToList();
            databaseContext.UserRoles.AddRange(userRoles);
            databaseContext.SaveChanges();


            var customers = new Faker<Customer>("de").CustomInstantiator(f =>
            {
                return new Customer
                {
                    User = f.Random.ListItem(users),
                    
                };
            }).Generate(10)
           .ToList();
            databaseContext.Customers.AddRange(customers);
            databaseContext.SaveChanges();


            var employeeCalendars = new Faker<EmployeeCalendar>("de").CustomInstantiator(f =>
            {
                return new EmployeeCalendar
                {
                    Date=f.Date.ToString(),
                    BookTime=f.PickRandom(20,50),
                    IsVacation= f.Random.Bool(0.00f),
                    //VacationDescription=null,
                    Employee= f.Random.ListItem(employees),
                };
            }).Generate(10)
          .ToList();
            databaseContext.EmployeeCalendars.AddRange(employeeCalendars);
            databaseContext.SaveChanges();

            var employeeTimes = new Faker<EmployeeTime>("de").CustomInstantiator(f =>
            {
                return new EmployeeTime
                {
                    Date = f.Date.ToString(),
                    Time = f.PickRandom("9:00", "10:00", "11:00"),
                    IsReserved = f.Random.Bool(0.00f),
                    //VacationDescription=null,
                    Employee = f.Random.ListItem(employees),
                };
            }).Generate(10)
         .ToList();
            databaseContext.EmployeeTimes.AddRange(employeeTimes);
            databaseContext.SaveChanges();

            

            foreach (var emp in employees)
            {
                if (emp.User.UserRoles is not null)
                {

                    emp.User.RoleId = emp.User.UserRoles.Select(x => x.RoleId).FirstOrDefault();
                }

                emp.User.RoleId = 3;
               

            }

            foreach (var cust in customers)
            {
                if (cust.User.UserRoles is not null)
                {

                    cust.User.RoleId = cust.User.UserRoles.Select(x => x.RoleId).FirstOrDefault();
                }

                cust.User.RoleId = 2;
            }


            databaseContext.SaveChanges();


            var bookings = new Faker<Booking>("de").CustomInstantiator(f =>
            {
                return new Booking
                {
                    Date = f.PickRandom("21.05.2024", "18.05.2024", "10.05.24", "01.11.2024"),
                    Time = f.PickRandom("9:00", "10:00", "11:00"),
                    Employee = f.Random.ListItem(employees),
                    Customer = f.Random.ListItem(customers),
                    Product = f.Random.ListItem(products),
                    Room = f.Random.ListItem(rooms)
                };

            }).Generate(10).ToList();
            databaseContext.Bookings.AddRange(bookings);
            databaseContext.SaveChanges();





            return databaseContext;

        }







    }
}
