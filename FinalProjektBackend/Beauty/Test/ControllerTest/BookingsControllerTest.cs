using AutoMapper;
using Beauty.Entity.Entities;
using Beauty.Repository.Contracts;
using Beauty.Shared.DTOs.Booking;
using Beauty.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.ControllerTest
{
    public class BookingsControllerTest
    {



        private readonly Mock<IBookingRepository> _bookingRepo;
        private readonly Mock<IAppointmentRepository> _appoRepo;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IEmployeeTimeRepository> _empTimeRepo;
        public BookingsControllerTest()
        {
            _bookingRepo = new Mock<IBookingRepository>();
            _appoRepo = new Mock<IAppointmentRepository>();
            _mapper = new Mock<IMapper>();
            _empTimeRepo = new Mock<IEmployeeTimeRepository>();
        }

        [Fact]
        public async Task BookingsController_GetBookings()
        {
            var bookings = Enumerable.Range(1, 3).Select(x => new Booking() { Id = x, Date="13.09.2024",Time="13:00", CustomerId = 250 + x, ProductId = 70 + x, EmployeeId = 90 + x, RoomId = 100 + x });
            var bookingDtos = Enumerable.Range(1, 3).Select(x => new BookingDto() { Id = x, Date = "13.09.2024", Time = "13:00", CustomerId = 250 + x, ProductId = 70 + x, EmployeeId = 90 + x, RoomId = 100 + x });

            _bookingRepo.Setup(x => x.GetBookingsAsync()).Returns(Task.FromResult(bookings));
            _mapper.Setup(x => x.Map<IEnumerable<BookingDto>>(bookings)).Returns(bookingDtos);

            var controller = new BookingsController(_bookingRepo.Object, _mapper.Object, _appoRepo.Object, _empTimeRepo.Object);
            //Act
            IActionResult actionResult = await controller.GetBookings();
            var OkResult = actionResult as OkObjectResult;
            var bDtos = OkResult.Value as IEnumerable<BookingDto>;
            //Assert
            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
            Assert.True(bDtos.Count()>0);

        }

        [Fact]
        public async Task BookingsController_GetBookingsByUserId() //(int userId)
        {
            var userId = 110;
            var bookings = Enumerable.Range(1, 2).Select(x => new Booking() { Id = x, Date = "13.0" + x + ".2024", Time = "13:00", CustomerId = userId, ProductId = x, EmployeeId = 60 + x, RoomId = 110 + x });
            var bookingDtos = Enumerable.Range(1, 2).Select(x => new BookingDto() { Id = x, Date = "13.0" +x+".2024", Time = "13:00",CustomerId = userId, ProductId =x, EmployeeId = 60 + x, RoomId = 110 + x });

            _bookingRepo.Setup(x => x.GetBookingsByUserIdAsync(userId)).Returns(Task.FromResult(bookings));
            _mapper.Setup(x => x.Map<IEnumerable<BookingDto>>(bookings)).Returns(bookingDtos);

            var controller = new BookingsController(_bookingRepo.Object, _mapper.Object, _appoRepo.Object, _empTimeRepo.Object);
            //Act
            IActionResult actionResult = await controller.GetBookingsByUserId(userId);
            var OkResult = actionResult as OkObjectResult;
            var bDtos = OkResult.Value as IEnumerable<BookingDto>;
            //Assert
            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
        }

        [Fact]
        public async Task GetBooking()  //(int employeeId, string date, string time)
        {
            //Arrange
            var empId = 101;
            var date = "09.13.2025";
            var time = "13:00";
            var booking = new Booking
            {
                Id = 1,
                Date = date,
                Time = time,
                CustomerId = 20,
                ProductId = 130,
                EmployeeId = empId,
                RoomId = 55,
            };
            var bookingDto = new BookingDto
            {
                Id = 1,
                Date = date,
                Time = time,
                CustomerId = 20,
                ProductId = 130,
                EmployeeId = empId,
                RoomId = 55,
            };

            _bookingRepo.Setup(x => x.GetBookingAsync(empId, date, time)).Returns(Task.FromResult(booking));
            _mapper.Setup(x => x.Map<BookingDto>(booking)).Returns(bookingDto);

            var controller = new BookingsController(_bookingRepo.Object, _mapper.Object, _appoRepo.Object, _empTimeRepo.Object);
            //Act
            IActionResult actionResult = await controller.GetBooking(empId, date, time);
            var OkResult = actionResult as OkObjectResult;
            var bDto = OkResult.Value as BookingDto;
            //Assert
            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
        }


        [Fact]
        public async Task BookingsController_GetBooking() //(int modelId)
        {
            var modelId = 1;
            var booking = new Booking
            {
                Id = modelId,
                Date = "13.09.2024",
                Time = "13:00",
                CustomerId = 20,
                ProductId = 130,
                EmployeeId = 40,
                RoomId = 55,
            };
            var bookingDto = new BookingDto
            {
                Id = modelId,
                Date = "13.09.2024",
                Time = "13:00",
                CustomerId = 20,
                ProductId = 130,
                EmployeeId = 40,
                RoomId = 55,
            };

            _bookingRepo.Setup(x => x.GetBookingAsync(modelId)).Returns(Task.FromResult(booking));
            _mapper.Setup(x => x.Map<BookingDto>(booking)).Returns(bookingDto);
            var controller = new BookingsController(_bookingRepo.Object, _mapper.Object, _appoRepo.Object, _empTimeRepo.Object);
            //Act
            IActionResult actionResult = await controller.GetBooking(modelId);
            var OkResult = actionResult as OkObjectResult;
            var bDto = OkResult.Value as BookingDto;
            //Assert
            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
            Assert.Equal(1, bDto.Id);
        }

        [Fact]
        public async Task BookingsController_CreateBooking() //([FromBody] BookingCreationDto model)
        {
            var model = new BookingCreationDto
            {
                Date = "20.04.2024",
                Time="13:00",
                CustomerId = 20,
                ProductId = 130,
                EmployeeId = 40,
                RoomId = 55,
                EmployeeTimeId=1,
            };

            var entity = new Booking()
            {
                Date = "20.04.2025",
                Time = "13:00",
                EmployeeId = model.EmployeeId,
                CustomerId = model.CustomerId,
                //AppointmentId = 10,
                ProductId = model.ProductId,
                RoomId = model.RoomId,
            };


            var empTime = new EmployeeTime
            {
                Id = 1,
                EmployeeId = 1,
                IsReserved = false,
                Date = "20.05.2024",
                Time="10:00"

            };

            _bookingRepo.Setup(x => x.CreateBookingAsync(It.IsAny<Booking>())).Returns(Task.CompletedTask);
            _bookingRepo.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);
            _mapper.Setup(x => x.Map<Booking>(model)).Returns(entity);
            _empTimeRepo.Setup(x => x.GetEmployeeTimeByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(empTime));
            _empTimeRepo.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);

                var controller = new BookingsController(_bookingRepo.Object, _mapper.Object, _appoRepo.Object,_empTimeRepo.Object);
            //Act
            IActionResult actionResult = await controller.CreateBooking(model);
            var statusCodeResult = Assert.IsType<StatusCodeResult>(actionResult);

            //Assert
            Assert.NotNull(actionResult);
            Assert.Equal(201, statusCodeResult.StatusCode);

        }


        [Fact]
        public async Task BookingsController_UpdateBooking() //(int modelId, [FromBody] BookingEditionDto model)
        {
            var modelId = 1;
            var model = new BookingEditionDto
            {
                Id = modelId,
                Date = "13.09.2024",
                Time = "13:00",
                CustomerId = 20,
                ProductId = 130,
                EmployeeId = 40,
                RoomId = 55,
            };

            var entity = new Booking()
            {
                Id = modelId,
                Date = "13.09.2024",
                Time = "13:00",
                EmployeeId = 40,
                CustomerId = 20,
                ProductId = 50,
                RoomId = 55,
            };

            _bookingRepo.Setup(x => x.GetBookingAsync(modelId)).Returns(Task.FromResult(entity));
            _bookingRepo.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);

            var controller = new BookingsController(_bookingRepo.Object, _mapper.Object, _appoRepo.Object,_empTimeRepo.Object);
            //Act
            IActionResult actionResult = await controller.UpdateBooking(modelId, model);
            var OkResult = actionResult as OkObjectResult;
            var value = OkResult.Value as string;
            //Assert
            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
            Assert.Equal("Edition done.", value);

        }

        [Fact]
        public async Task BookingsController_DeleteBooking() //(int modelId)
        {
            var modelId = 1;
            var entity = new Booking()
            {
                Id = modelId,
                Date = "13.09.2024",
                Time = "13:00",
                EmployeeId = 40,
                CustomerId = 20,
                ProductId = 50,
                RoomId = 55,
            };

            _bookingRepo.Setup(x => x.GetBookingAsync(modelId)).Returns(Task.FromResult(entity));
            _bookingRepo.Setup(x => x.DeleteBooking(entity)); //liefert void bzw.nichts, daher kein Return(..) oder sonstiges
            _bookingRepo.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);

            var controller = new BookingsController(_bookingRepo.Object, _mapper.Object, _appoRepo.Object,_empTimeRepo.Object);
            //Act
            IActionResult actionResult = await controller.DeleteBooking(modelId);
            var OkResult = actionResult as OkObjectResult;
            var value = OkResult.Value as string;
            //Assert
            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
            Assert.Equal("Deletion done.", value);

        }



        [Fact]
        public async Task GetBooking_ShouldPreventSQLInjection()  //(int employeeId, string date, string time)
        {
            //Arrange
            var empId = 101;
            var date = "09.13.2025";
            var malicioustime = "13:00'; DROP TABLE Customers--;";
           
            _bookingRepo.Setup(x => x.GetBookingAsync(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult((Booking)null));
            _mapper.Setup(x => x.Map<BookingDto>(It.IsAny<Booking>)).Returns((BookingDto)null);

            var controller = new BookingsController(_bookingRepo.Object, _mapper.Object, _appoRepo.Object, _empTimeRepo.Object);
            //Act
            IActionResult actionResult = await controller.GetBooking(empId, date, malicioustime);
            var notfound=  actionResult as NotFoundObjectResult;
            var notfoundValue = notfound.Value as string;
            //Assert
            Assert.NotNull(notfound);
            Assert.Equal(notfoundValue, "There is no data based on that id.");
        }










    }
}
