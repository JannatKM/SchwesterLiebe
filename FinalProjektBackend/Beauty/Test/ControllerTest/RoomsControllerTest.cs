using AutoMapper;
using Beauty.Entity.Entities;
using Beauty.Repository.Contracts;
using Beauty.Shared.DTOs.Room;
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
    public class RoomsControllerTest
    {
        private readonly Mock<IRoomRepository> _roomRepo;
        private readonly Mock<IMapper> _mapper;


        public RoomsControllerTest()
        {
            _roomRepo = new Mock<IRoomRepository>();
            _mapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task RoomsController_GetRooms()
        {
            var rooms = Enumerable.Range(1, 2).Select(x => new Room
            {
                Id = x,
                Name = "Raum" + x.ToString(),
            });

            _roomRepo.Setup(x => x.GetRoomsAsync()).Returns(Task.FromResult(rooms));

            var controller = new RoomsController(_roomRepo.Object,_mapper.Object);
            //Act
            IActionResult actionResult = await controller.GetRooms();
            var OkResult = actionResult as OkObjectResult;
            var roomDtos = OkResult.Value as IEnumerable<RoomDto>;
            //Assert
            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
            Assert.True(roomDtos.Count()>0);
        }


        [Fact]
        public async Task RoomsController_GetRoom()  //(int modelId)
        {
            var room = new Room
            {
                Id = 1,
                Name = "Raum23",
            };

            _roomRepo.Setup(x => x.GetRoomAsync(It.IsAny<int>())).Returns(Task.FromResult(room));
            var controller = new RoomsController(_roomRepo.Object, _mapper.Object);
            //Act
            IActionResult actionResult = await controller.GetRoom(1);
            var OkResult = actionResult as OkObjectResult;
            var roomDto = OkResult.Value as RoomDto;
            //Assert
            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
        }


        [Fact]
        public async Task RoomsController_CreateRoom()  //([FromBody] RoomCreationDto model)
        {
            var model = new RoomCreationDto()
            {
                Name = "Raum23"
            };

            var entity = new Room
            {
                Name = "Raum23"
            };
            _mapper.Setup(x => x.Map<Room>(model)).Returns(entity);
            _roomRepo.Setup(x => x.CreateRoomAsync(It.IsAny<Room>())).Returns(Task.CompletedTask);
            _roomRepo.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);
            var controller = new RoomsController(_roomRepo.Object, _mapper.Object);

            //Act
            IActionResult actionResult = await controller.CreateRoom(model);
            var statusCodeResult = Assert.IsType<StatusCodeResult>(actionResult);
            //Assert
            Assert.NotNull(actionResult);
            Assert.Equal(201, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task RoomsController_UpdateRoom() //(int modelId, [FromBody] RoomEditionDto model)
        {
            var room = new Room
            {
                Id = 7,
                Name = "RaumB",
            };

            var model = new RoomEditionDto
            {
                Id = 7,
                Name = "RaumA"
            };

            _roomRepo.Setup(x => x.GetRoomAsync(It.IsAny<int>())).Returns(Task.FromResult(room));
            _roomRepo.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);
            var controller = new RoomsController(_roomRepo.Object,_mapper.Object);
            //Act
            IActionResult actionResult = await controller.UpdateRoom(7, model);
            var OkResult = actionResult as OkObjectResult;
            var value = OkResult.Value as string;
            //Assert
            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
            Assert.Equal("Edition done.", value);
        }


        [Fact]
        public async Task RoomsController_DeleteRoom()  //(int modelId)
        {
            var room = new Room
            {
                Id = 3,
                Name = "RaumB",
            };

            _roomRepo.Setup(x => x.GetRoomAsync(It.IsAny<int>())).Returns(Task.FromResult(room));
            _roomRepo.Setup(x => x.DeleteRoom(It.IsAny<Room>()));
            _roomRepo.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);
            var controller = new RoomsController(_roomRepo.Object, _mapper.Object);
            //Act
            IActionResult actionResult = await controller.DeleteRoom(3);
            var OkResult = actionResult as OkObjectResult;
            var value = OkResult.Value as string;
            //Assert
            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
            Assert.Equal("Deletion done.", value);

        }










    }
}
