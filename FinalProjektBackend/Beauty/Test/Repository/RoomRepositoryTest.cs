using Beauty.Entity.Entities;
using Beauty.Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Repository
{
    public class RoomRepositoryTest
    {
        private RoomRepository _roomRepo;

        [Fact]
        public async Task CreateRoomAsync_DeleteRoom_GetRoomsAsync()
        {
            //Arrange
            var _db = await Context.GenerateContext();
            _roomRepo = new RoomRepository(_db);
            var id = 777;
            var room = new Room
            {
                Id=id,
                Name = "Testraum999",
                IsDown = false,
                Description = null
            };
            //Act
            await _roomRepo.CreateRoomAsync(room);
            await _db.SaveChangesAsync();
            var testroom= await _roomRepo.GetRoomAsync(id);
            //Assert Create & Get
            Assert.NotNull(testroom);
            Assert.Equal(id, testroom.Id);

            //Act
            _roomRepo.DeleteRoom(room);
            await _db.SaveChangesAsync();
            var testroomNull = await _roomRepo.GetRoomAsync(id);
            //Assert Delete & Get
            Assert.Null(testroomNull);
         
        }


        [Fact]
        public async Task GetRoomsAsync()
        {
            //Arrange
            var _db = await Context.Seed();
            _roomRepo = new RoomRepository(_db);
            //Act
            var rooms= await _roomRepo.GetRoomsAsync();
            //Assert
            Assert.NotNull(rooms);
            Assert.True(rooms.Count() > 0);

        }


        [Fact]
        public async Task GetRoomAsync_ShouldPreventSQLInjection()
        {
            //Arrange
            var _db = await Context.Seed();
            _roomRepo = new RoomRepository(_db);
            var maliciousInput = "1; DROP TABLE Employee; --";
            //Act & assert
            await Assert.ThrowsAsync<FormatException>(() => _roomRepo.GetRoomAsync(int.Parse(maliciousInput)));
        }




    }
}
