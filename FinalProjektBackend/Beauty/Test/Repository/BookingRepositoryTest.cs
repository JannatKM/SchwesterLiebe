using Beauty.Entity.Entities;
using Beauty.Repository.Services;
using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Repository
{
    public class BookingRepositoryTest
    {

       
        [Fact]
        public async void BookingRepository_CreateBookingAsync_And_DeleteBooking() // (Booking booking)
        {
            var booking = new Booking
            {
                Date = "10.9.2024",
                Time = "10:00",
                Employee = new Employee { User = new User { FirstName = "Max", LastName = "Müller", Email = "test@gmail.com", Telephone="12356",Password = "password123", RoleId = 3 } },
                Customer = new Customer { User = new User { FirstName = "Helga", LastName = "Müller", Email = "test234@gmail.com",Telephone="1234567", Password = "password123", RoleId = 2 } },
                Product = new Product { Name = "Laser", Description = "haarentfernung", Duration = 60, Price = 200 },
                Room = new Room { Name = "TestRaum237890", IsDown = false }
            };
            var dbContext = await  Context.GenerateContext();
            var bookingRepo = new BookingRepository(dbContext);
            //Act && Assert
            await bookingRepo.CreateBookingAsync(booking);
            await bookingRepo.SaveAsync();
            var raumName = dbContext.Bookings.Select(b => b.Room.Name).SingleOrDefault(r => r == "TestRaum237890");
            Assert.Equal("TestRaum237890", raumName);
            Assert.NotNull(raumName);

            bookingRepo.DeleteBooking(booking);
            await bookingRepo.SaveAsync();
            var raumNameNull = dbContext.Bookings.Select(b => b.Room.Name).SingleOrDefault(r => r == "TestRaum237890");
            Assert.Equal(raumNameNull, null);
          

        }

        [Fact]
        public async Task BookingRepository_GetBookingAsync()  //(int bookingId)
        {
            //Arrange
            var dbContext = await Context.Seed();
            var bookingRepo = new BookingRepository(dbContext);
            //Act
            var result=await bookingRepo.GetBookingAsync(1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Booking>(result);
            Assert.Equal(result.Id, 1);
        }


        [Fact]
        public async Task BookingRepository_GetBookingAsync2()  //(int employeeId, string date, string time)
        {
            //Arrange
            var dbContext = await Context.Seed();
            var bookingRepo = new BookingRepository(dbContext);
            var booking = new Booking
            {
                Id = 587,
                Date = "10.09.2024",
                Time = "10:00",
                Employee = new Employee { Id=927, User = new User { FirstName = "Max", LastName = "Müller", Email = "müller123@gmail.com",Telephone="+123456", Password = "password123", RoleId = 3 } },
                Customer = new Customer { User = new User { FirstName = "Helga", LastName = "Müller", Email = "helga123@gmail.com", Telephone="+123456", Password = "password123", RoleId = 2 } },
                Product = new Product { Name = "Laser", Description = "haarentfernung", Duration = 60, Price = 200 },
                Room = new Room { Name = "TestRaum237", IsDown = false }
            };
            await bookingRepo.CreateBookingAsync(booking);
            await bookingRepo.SaveAsync();
            //Act
            var result = await bookingRepo.GetBookingAsync(927, "10.09.2024", "10:00");
            //Assert
            Assert.NotNull(result);
            Assert.IsType<Booking>(result);
            Assert.Equal(result.Id, 587);

        }


        [Fact]
        public async Task BookingRepository_GetBookingsByUserIdAsync()  //(int userId)
        {
            //Arrange
            var dbContext = await Context.GenerateContext(); //Liefert ApplicationDbContext ohne Daten
            var bookingRepo = new BookingRepository(dbContext);
            var booking = new Booking
            {
                Id = 587,
                Date = "10.09.2024",
                Time = "10:00",
                Employee = new Employee { Id = 927, User = new User { FirstName = "Max", LastName = "Müller", Email = "müller123@gmail.com", Password = "password123", Telephone = "+0612345", RoleId = 3 } },
                Customer = new Customer { Id= 901, User = new User { FirstName = "Helga", LastName = "Müller", Email = "helga123@gmail.com", Password = "password123", Telephone = "061234567", RoleId = 2 } },
                Product = new Product { Name = "Laser", Description = "haarentfernung", Duration = 60, Price = 200 },
                Room = new Room { Name = "TestRaum237", IsDown = false }
            };
            await bookingRepo.CreateBookingAsync(booking);
            await bookingRepo.SaveAsync();

            //Act
            var bookings = await bookingRepo.GetBookingsByUserIdAsync(901);
            //Assert
            Assert.NotNull(bookings);
            Assert.IsType<List<Booking>>(bookings);
           
        }


        [Fact]  
        public async Task BookingRepository_GetBookingsAsync()
        {
            //Arrange
            var dbContext = await Context.Seed(); //Liefert ApplicationDbContext mit Daten
            var bookingRepo = new BookingRepository(dbContext);
            //Act
            var bookings = await bookingRepo.GetBookingsAsync();
            //Assert
            Assert.NotNull(bookings);
            Assert.IsType<List<Booking>>(bookings);
           
        }



        [Fact]
        public async Task BookingRepository_GetBookingAsync_ShouldPreventSQLInjection() 
        {
            // Arrange
            var dbContext = await Context.Seed(); //Liefert ApplicationDbContext mit Daten
            var bookingRepo = new BookingRepository(dbContext);
            var maliciousInput = "1; DROP TABLE Bookings; --";
            //Act & assert
            await Assert.ThrowsAsync<FormatException>(() => bookingRepo.GetBookingAsync(int.Parse(maliciousInput)));
        }


        [Fact]
        public async Task BookingRepository_GetBookingAsync2_ShouldPreventSQLInjection()  //(int employeeId, string date, string time)
        {
            //Arrange
            var dbContext = await Context.Seed();
            var bookingRepo = new BookingRepository(dbContext);
            var maliciousInput = "10:00'; DROP TABLE Employees ; --";
            var booking= await  bookingRepo.GetBookingAsync(927, "10.09.2024", maliciousInput);
            // assert & act
            Assert.Null(booking); 
        }


        [Fact]
        public async Task BookingRepository_GetBookingsByUserIdAsync_ShouldPreventSQLInjection()  //(int userId)
        {
            //Arrange
            var dbContext = await Context.GenerateContext(); //Liefert ApplicationDbContext ohne Daten
            var bookingRepo = new BookingRepository(dbContext);
            var maliciousInput = "1; DROP TABLE Bookings; --";
            //Act & assert
            await Assert.ThrowsAsync<FormatException>(() => bookingRepo.GetBookingsByUserIdAsync(int.Parse(maliciousInput)));
        }







    }
}
