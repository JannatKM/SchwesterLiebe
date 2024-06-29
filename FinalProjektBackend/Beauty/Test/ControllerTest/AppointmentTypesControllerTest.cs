using Beauty.Entity.Entities;
using Beauty.Repository.Contracts;
using Beauty.Shared.DTOs.AppointmentType;
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
    public class AppointmentTypesControllerTest
    {
        private readonly Mock<IAppointmentTypeRepository> _appoTypeRepo;

        public AppointmentTypesControllerTest()
        {
            _appoTypeRepo = new Mock<IAppointmentTypeRepository>();
        }


        [Fact]
        public async Task AppointmentTypesController_GetAppointmentTypes()
        {
            //Arrange
            var appoTypes = Enumerable.Range(1, 2).Select(x => new AppointmentType { Id = x, Type = "Laser" });

            _appoTypeRepo.Setup(x => x.GetAppointmentTypesAsync()).Returns(Task.FromResult(appoTypes));

            var controller = new AppointmentTypesController(_appoTypeRepo.Object);
            //Act
            IActionResult actionResult = await controller.GetAppointmentTypes();
            var OkResult = actionResult as OkObjectResult;
            var values = OkResult.Value as IEnumerable<AppointmentTypeDto>;
            //Assert
            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
            Assert.Equal(2, values.Count());

        }

        [Fact]
        public async Task AppointmentTypesController_GetAppointmentType() //(int modelId)
        {
            //Arrange
            var appoType = new AppointmentType { Id = 1, Type = "Waxing" };
            
            _appoTypeRepo.Setup(x => x.GetAppointmentTypeAsync(It.IsAny<int>())).Returns(Task.FromResult(appoType));
            var controller = new AppointmentTypesController(_appoTypeRepo.Object);
            //Act
            IActionResult actionResult = await controller.GetAppointmentType(1);
            var OkResult = actionResult as OkObjectResult;
            var appoTypeDto = OkResult.Value as AppointmentTypeDto;
            //Assert
            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
            Assert.Equal(1, appoTypeDto.Id);
        }


        [Fact]
        public async Task AppointmentTypesController_CreateAppointmentType() //([FromBody] AppointmentTypeCreationDto model)
        {
            //Arrange
            var model = new AppointmentTypeCreationDto
            {
                Type = "Waxing"
            };

            _appoTypeRepo.Setup(x => x.CreateAppointmentTypeAsync(It.IsAny<AppointmentType>())).Returns(Task.CompletedTask);
            _appoTypeRepo.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);
            var controller = new AppointmentTypesController(_appoTypeRepo.Object);
            //Act
            IActionResult actionResult = await controller.CreateAppointmentType(model);
            var statusCodeResult = Assert.IsType<StatusCodeResult>(actionResult);
            //Assert
            Assert.NotNull(actionResult);
            Assert.Equal(201, statusCodeResult.StatusCode);
        }


        [Fact]
        public async Task AppointmentTypesController_UpdateAppointmentType() //(int modelId, [FromBody] AppointmentTypeEditionDto model)
        {

            var model = new AppointmentTypeEditionDto
            {
                Id = 1,
                Type = "Waxing"
            };
          
            var entity = new AppointmentType()
            {
                Id = 1,
                Type = "Laser"
            };
            _appoTypeRepo.Setup(x => x.GetAppointmentTypeAsync(It.IsAny<int>())).Returns(Task.FromResult(entity));
            _appoTypeRepo.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);
            var controller = new AppointmentTypesController(_appoTypeRepo.Object);

            //Act
            IActionResult actionResult = await controller.UpdateAppointmentType(1, model);
            var OkResult = actionResult as OkObjectResult;
            var value = OkResult.Value as string;
            //Assert

            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
            Assert.Equal("Edition done.", value);

        }


        [Fact]
        public async Task AppointmentTypesController_DeleteAppointmentType() //(int modelId)
        {
           
            var entity = new AppointmentType()
            {
                Id = 1,
                Type = "Laser"
            };

            _appoTypeRepo.Setup(x => x.GetAppointmentTypeAsync(It.IsAny<int>())).Returns(Task.FromResult(entity));
            _appoTypeRepo.Setup(x => x.DeleteAppointmentType(entity));
            _appoTypeRepo.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);

            var controller = new AppointmentTypesController(_appoTypeRepo.Object);
            //Act
            IActionResult actionResult = await controller.DeleteAppointmentType(1);
            var OkResult = actionResult as OkObjectResult;
            var value = OkResult.Value as string;
            //Assert
            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
            Assert.Equal("Deletion done.", value);
        }














    }
}
