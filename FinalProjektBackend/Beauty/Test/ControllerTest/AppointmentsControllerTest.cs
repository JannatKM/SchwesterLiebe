using AutoMapper;
using Beauty.Entity.Entities;
using Beauty.Repository.Contracts;
using Beauty.Shared.DTOs.Appointment;
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
    public class AppointmentsControllerTest
    {
        private readonly Mock<IAppointmentRepository> _appointmentRepo;
        private readonly Mock<IDiscountRepository> _discountRepo;
        private readonly Mock<IProductRepository> _productRepo;
        private readonly Mock<IMapper> _mapper;

        public AppointmentsControllerTest()
        {
            _appointmentRepo = new Mock<IAppointmentRepository>();
            _discountRepo = new Mock<IDiscountRepository>();
            _productRepo = new Mock<IProductRepository>();
            _mapper = new Mock<IMapper>();
        }



        [Fact]
        public async Task AppointmentsController_GetAppointments()
        {

            //Arrange
            var appointments = Enumerable.Range(1, 2).Select(x => new Appointment()
            {
                Id = x,
                IsSelected = false,
                Date = "2024-05-01",
                StartTime = "09:00",
                EndTime = "10:00",
                EmployeeId = 123 + x,
            });
            var appointmentDtos = Enumerable.Range(1, 2).Select(x => new AppointmentDto()
            {
                Id = x,
                IsSelected = false,
                Date = "2024-05-01",
                StartTime = "09:00",
                EndTime = "10:00",
                EmployeeId = 123 + x,
            });


            _appointmentRepo.Setup(x => x.GetAppointmentsAsync()).Returns(Task.FromResult(appointments));
            _mapper.Setup(x => x.Map<IEnumerable<AppointmentDto>>(appointments)).Returns(appointmentDtos);

            var controller = new AppointmentsController(_appointmentRepo.Object, _mapper.Object, _discountRepo.Object, _productRepo.Object);
            //Act
            IActionResult actionResult = await controller.GetAppointments();
            var OkResult = actionResult as OkObjectResult;
            var appoDtos = OkResult.Value as IEnumerable<AppointmentDto>;
            //Assert
            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
            Assert.Equal(2, appoDtos.Count());

        }


        [Fact]
          public async Task GetEmployeeAppointments()  //(int id)
        {
            //Arrange
            var appointments = Enumerable.Range(1, 2).Select(x => new Appointment()
            {
                Id = x,
                IsSelected = false,
                Date = "2024-05-01",
                StartTime = "09:00",
                EndTime = "10:00",
                EmployeeId = 123,
            });
            var appointmentDtos = Enumerable.Range(1, 2).Select(x => new AppointmentDto()
            {
                Id = x,
                IsSelected = false,
                Date = "2024-05-01",
                StartTime = "09:00",
                EndTime = "10:00",
                EmployeeId = 123 ,
            });

            _appointmentRepo.Setup(x=>x.GetAppointmentsByEmployeeIdAsync(It.IsAny<int>())).Returns(Task.FromResult(appointments));
            _mapper.Setup(x => x.Map<IEnumerable<AppointmentDto>>(appointments)).Returns(appointmentDtos);
            var controller = new AppointmentsController(_appointmentRepo.Object, _mapper.Object, _discountRepo.Object, _productRepo.Object);
            //Act
            var actionResult = await controller.GetEmployeeAppointments(123);
            var OkResult = actionResult as OkObjectResult;
            var appoDtos = OkResult.Value as IEnumerable<AppointmentDto>;
            //Assert
            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
            Assert.Equal(2, appoDtos.Count());

        }





        [Fact]
        public async Task AppointmentsController_GetLastThreeAppointments()
        {
            //Arrange

            var appointments = Enumerable.Range(1, 3).Select(x => new Appointment()
            {
                Id = x,
                IsSelected = false,
                Date = "2024-05-01",
                StartTime = "09:00",
                EndTime = "10:00",
                EmployeeId = 123 + x,
                RoomId = 16 + x,
                AppointmentTypeId = 29 + x,
                ProductId = 5 + x
            });
            var appointmentDtos = Enumerable.Range(1, 3).Select(x => new AppointmentDto()
            {
                Id = x,
                IsSelected = false,
                Date = "2024-05-01",
                StartTime = "09:00",
                EndTime = "10:00",
                EmployeeId = 123 + x,
                RoomId = 16 + x,
                AppointmentTypeId = 29 + x,
                ProductId = 5 + x
            });

            _appointmentRepo.Setup(x => x.GetLastThreeAppointmentsAsync()).Returns(Task.FromResult(appointments));
            _mapper.Setup(x => x.Map<IEnumerable<AppointmentDto>>(appointments)).Returns(appointmentDtos);

            var controller = new AppointmentsController(_appointmentRepo.Object, _mapper.Object, _discountRepo.Object, _productRepo.Object);
            //Act
            IActionResult actionResult = await controller.GetLastThreeAppointments();
            var OkResult = actionResult as OkObjectResult;
            var appoDtos = OkResult.Value as IEnumerable<AppointmentDto>;

            //Assert
            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
            Assert.Equal(3, appoDtos.Count());

        }


        [Fact]
        public async Task AppointmentsController_GetAppointment() //(int modelId)
        {
            var appointment = new Appointment()
            {
                Id = 1,
                Date = "2024-05-01",
                StartTime = "09:00",
                EndTime = "10:00",
                EmployeeId = 123,
                RoomId = 16,
                AppointmentTypeId = 29,
                ProductId = 5
            };
            var AppointmentDto = new AppointmentDto()
            {
                Id = 1,
                Date = "2024-05-01",
                StartTime = "09:00",
                EndTime = "10:00",
                EmployeeId = 123,
                RoomId = 16,
                AppointmentTypeId = 29,
                ProductId = 5
            };

            var modeID = 1;
            _appointmentRepo.Setup(x => x.GetAppointmentAsync(modeID)).Returns(Task.FromResult(appointment));
            _mapper.Setup(x => x.Map<AppointmentDto>(appointment)).Returns(AppointmentDto);

            var controller = new AppointmentsController(_appointmentRepo.Object, _mapper.Object, _discountRepo.Object, _productRepo.Object);

            //Act
            IActionResult actionResult = await controller.GetAppointment(1);
            var OkResult = actionResult as OkObjectResult;
            var appointmentDto = OkResult.Value as AppointmentDto;
            //Assert

            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
        }




        [Fact]
        public async Task AppointmentsController_CreateAppointment() //AppointmentCreationDto model
        {
            //Arrange
            var model = new AppointmentCreationDto
            {
                IsSelected = false,
                Date = "2024-05-01",
                StartTime = "09:00",
                EndTime = "10:00",
                EmployeeId = 123,
                RoomId = 16,
                AppointmentTypeId = 29,
                ProductId = 5
            };

            var entity = new Appointment()
            {
                Date = model.Date,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                EmployeeId = model.EmployeeId,
                RoomId = model.RoomId,
                AppointmentTypeId = model.AppointmentTypeId,
                ProductId = model.ProductId,
                //DiscountId = item.Id
            };

            var discounts = Enumerable.Range(1, 3).Select(x => new Discount() { Id = x, Percent = 20, EndDate="09.2.2025", StartDate="28.02.2025" });

            _appointmentRepo.Setup(x => x.CreateAppointmentAsync(entity)).Returns(Task.CompletedTask);
            _discountRepo.Setup(x => x.GetDiscountsAsync()).Returns(Task.FromResult(discounts));
            _appointmentRepo.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);

            var controller = new AppointmentsController(_appointmentRepo.Object, _mapper.Object, _discountRepo.Object, _productRepo.Object);
            //Act
            IActionResult actionResult = await controller.CreateAppointment(model);
            var statusCodeResult = Assert.IsType<StatusCodeResult>(actionResult);

            //Assert
            Assert.NotNull(actionResult);
            Assert.Equal(201, statusCodeResult.StatusCode);

        }


        [Fact]
        public async Task AppointmentsController_UpdateAppointment()  //(int modelId, [FromBody] AppointmentEditionDto model)
        {
            var modelId = 1;

            var model = new AppointmentEditionDto
            {
                Id = 1,
                IsSelected = false,
                Date = "2024-05-01",
                StartTime = "09:00",
                EndTime = "10:00",
                EmployeeId = 123,
                RoomId = 16,
                AppointmentTypeId = 29,
                ProductId = 5
            };

            var editingModel = new Appointment
            {
                Id = 1,
                Date = "2024-05-01",
                StartTime = "08:00",
                EndTime = "08:40",
                EmployeeId = 123,
                RoomId = 16,
                AppointmentTypeId = 29,
                ProductId = 5,
                //DiscountId = item.Id
            };

            _appointmentRepo.Setup(x => x.GetAppointmentAsync(modelId)).Returns(Task.FromResult(editingModel));
            _appointmentRepo.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);

            var controller = new AppointmentsController(_appointmentRepo.Object, _mapper.Object, _discountRepo.Object, _productRepo.Object);

            //Act
            IActionResult actionResult = await controller.UpdateAppointment(modelId, model);
            var OkResult = actionResult as OkObjectResult;
            var value = OkResult.Value as string;
            //Assert

            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
            Assert.Equal("Edition done.", value);

        }


        [Fact]
        public async Task AppointmentsController_DeleteAppointment()  //(int modelId)
        {
            //Arrange
            var modelId = 2;
            var appo = new Appointment
            {
                Id = 2,
                Date = "2024-05-01",
                StartTime = "09:00",
                EndTime = "10:00",
                EmployeeId = 123,
                RoomId = 16,
                AppointmentTypeId = 29,
                ProductId = 5
            };

            _appointmentRepo.Setup(x => x.GetAppointmentAsync(modelId)).Returns(Task.FromResult(appo));
            _appointmentRepo.Setup(x => x.DeleteAppointment(appo)); //liefert void
            _appointmentRepo.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);

            var controller = new AppointmentsController(_appointmentRepo.Object, _mapper.Object, _discountRepo.Object, _productRepo.Object);

            //Act
            IActionResult actionResult = await controller.DeleteAppointment(modelId);
            var OkResult = actionResult as OkObjectResult;
            var value = OkResult.Value as string;
            //Assert
            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
            Assert.Equal("Deletion done.", value);

        }



    }






}

