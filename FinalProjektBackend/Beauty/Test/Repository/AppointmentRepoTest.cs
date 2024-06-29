using Beauty.Entity.Entities;
using Beauty.Repository.Data;
using Beauty.Repository.Services;
using Bogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;



namespace Test.Repository
{
    public class AppointmentRepoTest
    {
    
        [Fact]
        public async Task AppointmentRepository_GetAppointmentAsync()  //(int appointmentId)
        {
            //Arrange
            var dbContext = await Context.Seed();
            var appointmentRepo = new AppointmentRepository(dbContext);
            //Act
            var result = await appointmentRepo.GetAppointmentAsync(1);
            //Assert
            Assert.IsType<Appointment>(result);
            Assert.NotNull(result);
            Assert.Equal(result.Id, 1);

        }


        [Fact]
        public async Task AppointmentRepository_CreateAppointmentAsync()  //(Appointment appointment)
        {
            //Arrange
        
            var appo = new Appointment
            {  
                IsSelected = true,
                Date = "2024-05-12",
                StartTime = "10:00 AM",
                EndTime = "11:00 AM",
                // Kein Rabatt für diese Buchung
                Room = new Room { Name = "TestRaum", IsDown=false},
                AppointmentType = new AppointmentType { Type = "Laser" },
                Product = new Product { Name = "Laser", Description = "haarentfernung", Duration=60, Price=200 },
                Employee = new Employee { User = new User { FirstName = "Max", LastName = "Müller", Email = "test@gmail.com", Telephone="+123456", Password = "password123", RoleId=3 } },

            };
            var dbContext =  await Context.GenerateContext();
            var appointmentRepo = new AppointmentRepository(dbContext);
            //Act
            await appointmentRepo.CreateAppointmentAsync(appo);
            await appointmentRepo.SaveAsync();
            //Assert
            Assert.True(dbContext.Appointments.Count()>0);
       
        }


        [Fact]
        public async void AppointmentRepository_DeleteAppointment() //(Appointment appointment)
        {
            //Arrange
            var appo = new Appointment
            {
                Id=987,
                IsSelected = true,
                Date = "2024-05-12",
                StartTime = "10:00 AM",
                EndTime = "11:00 AM",
                // Kein Rabatt für diese Buchung
                Room = new Room { Name = "TestRaum", IsDown = false },
                AppointmentType = new AppointmentType { Type = "Laser" },
                Product = new Product { Name = "Laser", Description = "haarentfernung", Duration = 60, Price = 200 },
                Employee = new Employee { User = new User { FirstName = "Max", LastName = "Müller", Email = "test@gmail.com", Telephone="+12345",Password = "password123"} },

            };

            var dbContext = await Context.GenerateContext(); 
            var appointmentRepo = new AppointmentRepository(dbContext);
            //Act &Assert
            await appointmentRepo.CreateAppointmentAsync(appo);
            await appointmentRepo.SaveAsync();
            Assert.True(dbContext.Appointments.Count()>0);

            appointmentRepo.DeleteAppointment(appo); 
            await appointmentRepo.SaveAsync();
            var appointmentNull = await appointmentRepo.GetAppointmentAsync(987);
            Assert.Null(appointmentNull);

        }

        [Fact]
        public async Task AppointmentRepository_GetAppointmentsAsync() 
        {
            //Arrange
            var _db = await Context.Seed() ; //Liefert ApplicationDbContext mit Daten
            var appoRepo = new AppointmentRepository(_db);
            //Act
            var appointments = await appoRepo.GetAppointmentsAsync();
            //Assert
            Assert.IsType<List<Appointment>>(appointments);
            Assert.NotNull(appointments);
           

        }


        [Fact]
        public async Task AppointmentRepository_GetAppointmentsByEmployeeIdAsync() //(int id)
        {
            //Arrange
            var _db = await Context.Seed(); //Liefert ApplicationDbContext mit Daten
            var appoRepo = new AppointmentRepository(_db);
            //Act
            var appointments = await appoRepo.GetAppointmentsByEmployeeIdAsync(1); //Todo
            //Assert
            Assert.IsType<List<Appointment>>(appointments);
            Assert.NotNull(appointments);
        }

        [Fact]
        public async Task AppointmentRepository_GetLastThreeAppointmentsAsync()
        {
            //Arrange
            var _db = await Context.Seed();//Liefert ApplicationDbContext mit Daten
            var appoRepo = new AppointmentRepository(_db);
            //Act
            var appointments = await appoRepo.GetLastThreeAppointmentsAsync();
            //Assert
            Assert.IsType<List<Appointment>>(appointments);
            Assert.NotNull(appointments);
            Assert.True(appointments.Count()==3);
        }




        [Fact]
        public async Task AppointmentRepository_GetAppointmentAsync_ShouldPreventSQLInjection() //Beispiel Sql-Injection
        {
            // Arrange
            var _db = await Context.Seed();
            var appoRepo = new AppointmentRepository(_db);
            var maliciousInput = "1; DROP TABLE Appointments; --";
            //Act & assert
            await Assert.ThrowsAsync<FormatException>(() => appoRepo.GetAppointmentAsync(int.Parse(maliciousInput)));
        }



        [Fact]
        public async Task AppointmentRepository_GetAppointmentsByEmployeeIdAsync_ShouldPreventSQLInjection() //(int id)
        {
            //Arrange
            var _db = await Context.Seed(); //Liefert ApplicationDbContext mit Daten
            var appoRepo = new AppointmentRepository(_db);
            //Act
            var maliciousInput = "1; DROP TABLE Appointments; --"; 
            // Act & Assert
            await Assert.ThrowsAsync<FormatException>(() => appoRepo.GetAppointmentsByEmployeeIdAsync(int.Parse(maliciousInput)));
        }











    }
}
