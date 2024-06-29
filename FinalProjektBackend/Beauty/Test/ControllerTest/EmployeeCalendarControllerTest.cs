using AutoMapper;
using Beauty.Entity.Entities;
using Beauty.Repository.Contracts;
using Beauty.Shared.DTOs.Booking;
using Beauty.Shared.DTOs.EmployeeCalendar;
using Beauty.Shared.DTOs.EmployeeTime;
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
    public  class EmployeeCalendarControllerTest
    {

        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IEmployeeTimeRepository> _empTimeRepo;
        private readonly Mock<IEmployeeCalendarRepository> _empCRepo;


      
        public EmployeeCalendarControllerTest()
        {
            _mapper = new Mock<IMapper>();
            _empTimeRepo = new Mock<IEmployeeTimeRepository>();
            _empCRepo = new Mock<IEmployeeCalendarRepository>();
        }


        [Fact]
        public async Task GetEmployeeTimes()  //(string date)
        {
            //Arrange
            var date = "13.08.2025";
            var empTimes = Enumerable.Range(1, 2).Select(x => new EmployeeTime
            {
                Id=x,
                Date=date,
                Time="0"+x+":00",
                IsReserved=false,
                EmployeeId=x

            });
            _empTimeRepo.Setup(x => x.GetEmployeeTimesByDateAsync(date)).Returns(Task.FromResult(empTimes));
            var controller = new EmployeeCalendarController(_empCRepo.Object, _mapper.Object, _empTimeRepo.Object);
            //Act
             var actionResult = await controller.GetEmployeeTimes(date);
            var OkResult = actionResult as OkObjectResult;
            var results = OkResult.Value as IEnumerable<EmployeeTime>;
            //Assert
            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
        }

        [Fact]
        public async Task GetEmployeeTimes2()    //(int id, string date)
        {
            //Arrange
            var date = "13.08.2025";
            var id = 1;
            var empTimes = Enumerable.Range(1, 2).Select(x => new EmployeeTime
            {
                Id = x,
                Date = date,
                Time = "1" + x + ":00",
                IsReserved = false,
                EmployeeId = id

            });
            _empTimeRepo.Setup(x => x.GetEmployeeTimesByDateAsync(id,date)).Returns(Task.FromResult(empTimes));
            var controller = new EmployeeCalendarController(_empCRepo.Object, _mapper.Object, _empTimeRepo.Object);
            //Act
            var actionResult = await controller.GetEmployeeTimes(id,date);
            var OkResult = actionResult as OkObjectResult;
            var results = OkResult.Value as IEnumerable<EmployeeTime>;
            //Assert
            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
       }



        [Fact]
        public async Task GetEmployeeCalendars()
        {
            //Arrange
            var empCTimes = Enumerable.Range(1, 2).Select(x => new EmployeeCalendar
            {
                Id=x,
                Date = "2024-06-2"+x,
                BookTime = 14,
                EmployeeId = x,
                Employee = new Employee { Id = x, User = new User { Id = x } }
            });
            var empCTimeDtos = Enumerable.Range(1, 2).Select(x => new EmployeeCalendarDto
            {
                Id = x,
                Date = "2024-06-2" + x,
                BookTime = 14,
                EmployeeId = x,
            });
            _empCRepo.Setup(x => x.GetEmployeeCalendarsAsync()).Returns(Task.FromResult(empCTimes));
            _mapper.Setup(x => x.Map<IEnumerable<EmployeeCalendarDto>>(empCTimes)).Returns(empCTimeDtos);
            var controller = new EmployeeCalendarController(_empCRepo.Object, _mapper.Object, _empTimeRepo.Object);
            //Act
            var actionResult = await controller.GetEmployeeCalendars();
            var OkResult = actionResult as OkObjectResult;
            var results = OkResult.Value as IEnumerable<EmployeeCalendarDto>;
            //Assert
            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
        }

        [Fact]
        public async Task GetEmployeeTimesDate()   //(int id)
        {
            //Arrange
            var empCTimes = Enumerable.Range(1, 2).Select(x => new EmployeeCalendar
            {
                Id = x,
                Date = "2024-06-2" + x,
                BookTime = 14,
                EmployeeId = 1,
                Employee = new Employee { Id = 1, User = new User { Id = 1 } }
            });
         
            _empCRepo.Setup(x => x.GetDateByEmployeeIdAsync(It.IsAny<int>())).Returns(Task.FromResult(empCTimes));
            
            var controller = new EmployeeCalendarController(_empCRepo.Object, _mapper.Object, _empTimeRepo.Object);
            //Act
            var actionResult = await controller.GetEmployeeTimesDate(It.IsAny<int>());
            var OkResult = actionResult as OkObjectResult;
            var results = OkResult.Value as IEnumerable<EmployeeCalendar>;
            //Assert
            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
            
        }

        [Fact]
        public async Task  GetEmployeeTimesByDate() //(string date)
        {
            //Arrange
            var empTimes = Enumerable.Range(1, 2).Select(x => new EmployeeTime
            {
                Id = x,
                Date = "2024-06-2",
                Time = "1" + x + ":00",
                IsReserved = false,
                EmployeeId = x
            });
            _empTimeRepo.Setup(x => x.GetEmployeeTimesByDateAsync(It.IsAny<string>())).Returns(Task.FromResult(empTimes));

            var controller = new EmployeeCalendarController(_empCRepo.Object, _mapper.Object, _empTimeRepo.Object);
            //Act
            var actionResult = await controller.GetEmployeeTimesByDate(It.IsAny<string>());
            var OkResult = actionResult as OkObjectResult;
            var results = OkResult.Value as IEnumerable<EmployeeTime>;
            //Assert
            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
        }

        [Fact]
        public async Task GetEmployeeCalendars2()   //(int id)
        {
            //Arrange
            var empCTimes = Enumerable.Range(1, 2).Select(x => new EmployeeCalendar
            {
                Id = x,
                Date = "2024-06-2" + x,
                BookTime = 14,
                EmployeeId = 1,
                Employee = new Employee { Id = 1, User = new User { Id = 1 } }
            });
            var empCTimeDtos = Enumerable.Range(1, 2).Select(x => new EmployeeCalendarDto
            {
                Id = x,
                Date = "2024-06-2" + x,
                BookTime = 14,
                EmployeeId = 1,
            });
            _empCRepo.Setup(x => x.GetEmployeeCalendarsByEmployeeIdAsync(It.IsAny<int>())).Returns(Task.FromResult(empCTimes));
            _mapper.Setup(x => x.Map<IEnumerable<EmployeeCalendarDto>>(empCTimes)).Returns(empCTimeDtos);
            var controller = new EmployeeCalendarController(_empCRepo.Object, _mapper.Object, _empTimeRepo.Object);
            //Act
            var actionResult = await controller.GetEmployeeCalendars(It.IsAny<int>());
            var OkResult = actionResult as OkObjectResult;
            var results = OkResult.Value as IEnumerable<EmployeeCalendarDto>;
            //Assert
            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
            
        }

        [Fact]
        public async Task GetEmployeeCalendar()    //(int modelId)
        {
             //Arrange
            var empTime = new EmployeeTime
            {
                Id = 1,
                Date = "2024-06-2",
                EmployeeId = 1,
                Employee = new Employee { Id = 1, User = new User { Id = 1 } }
            };
            var empTimeDto = new EmployeeTimeDto
            {
                Id = 1,
                Date = "2024-06-2",
                EmployeeId = 1,
            };

        _empTimeRepo.Setup(x => x.GetEmployeeTimeByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(empTime));
        _mapper.Setup(x => x.Map<EmployeeTimeDto>(empTime)).Returns(empTimeDto);
        var controller = new EmployeeCalendarController(_empCRepo.Object, _mapper.Object, _empTimeRepo.Object);
        //Act
        var actionResult = await controller.GetEmployeeCalendar(1);
        var OkResult = actionResult as OkObjectResult;
        var results = OkResult.Value as EmployeeTimeDto;
        //Assert
        Assert.NotNull(OkResult);
        Assert.NotNull(OkResult.Value);

        }

        [Fact]
        public async Task CreateEmployeeCalendar ()  //( [FromBody] EmployeeCalendarForCreationDto model)
        {
            //Arrange
            var empCTime = new EmployeeCalendar
            {
                Id = 1,
                Date = "2024-06-2",
                BookTime = 14,
                EmployeeId = 1,
                IsVacation = true,
                VacationDescription="Urlaub",
                Employee = new Employee { Id = 1, User = new User { Id = 1 } }
            };

            _empCRepo.Setup(x => x.CreateEmployeeCalendarAsync(It.IsAny<EmployeeCalendar>())).Returns(Task.CompletedTask);
            _mapper.Setup(x => x.Map<EmployeeCalendar>(It.IsAny<EmployeeCalendarForCreationDto>())).Returns(empCTime);
            _empCRepo.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);

            var controller = new EmployeeCalendarController(_empCRepo.Object, _mapper.Object, _empTimeRepo.Object);
            //Act
            var actionResult = await controller.CreateEmployeeCalendar(It.IsAny<EmployeeCalendarForCreationDto>());
            var statusCodeResult = Assert.IsType<StatusCodeResult>(actionResult);
            //Assert
            Assert.NotNull(actionResult);
            Assert.Equal(201, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task UpdateEmployeeCalendar()//(int modelId,[FromBody] EmployeeCalendarForEditionDto model)
        {
            //Arrange
            var empC = new EmployeeCalendar
            {
                Id = 1,
                Date = "2024-06-2",
                EmployeeId = 1,
                Employee = new Employee { Id = 1, User = new User { Id = 1 } }
            };
            _empCRepo.Setup(x => x.GetEmployeeCalendarAsync(It.IsAny<int>())).Returns(Task.FromResult(empC));
            _mapper.Setup(x => x.Map(It.IsAny<EmployeeCalendarForEditionDto>(), empC));
            _empCRepo.Setup(x=>x.SaveAsync()).Returns(Task.CompletedTask);
            var controller = new EmployeeCalendarController(_empCRepo.Object, _mapper.Object, _empTimeRepo.Object);
            //Act
            var actionResult = await controller.UpdateEmployeeCalendar(It.IsAny<int>(),It.IsAny<EmployeeCalendarForEditionDto>());
            var statusCodeResult = Assert.IsType<StatusCodeResult>(actionResult);
            //Assert
            Assert.NotNull(actionResult);
            Assert.Equal(200, statusCodeResult.StatusCode);

        }

        [Fact]
        public async Task DeleteEmployeeCalendar() //(int modelId)
        {
            //Arrange
            var empC = new EmployeeCalendar
            {
                Id = 1,
                Date = "2024-06-2",
                EmployeeId = 1,
                Employee = new Employee { Id = 1, User = new User { Id = 1 } }
            };
            _empCRepo.Setup(x => x.GetEmployeeCalendarAsync(It.IsAny<int>())).Returns(Task.FromResult(empC));
            _empCRepo.Setup(x => x.DeleteEmployeeCalendar(It.IsAny<EmployeeCalendar>()));
            _empCRepo.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);
            var controller = new EmployeeCalendarController(_empCRepo.Object, _mapper.Object, _empTimeRepo.Object);
            //Act
            var actionResult = await controller.DeleteEmployeeCalendar(It.IsAny<int>());
            var OkResult = actionResult as OkObjectResult;
            var results = OkResult.Value as string;
            //Assert
            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
        }








    }
}
