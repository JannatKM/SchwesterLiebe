using Beauty.Entity.Entities;
using Beauty.Repository.Data;
using Beauty.Repository.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Repository
{
    public class AppointmentTypeRepositoryTest
    {
       

        [Fact]
        public async Task AppointmentTypeRepository_CreateAppointmentTypeAsync() //(AppointmentType appointmentType)
        {
            //Arrange
            var appoType = new AppointmentType
            {
                Type="Laser",
            };

            var dbContext = await Context.GenerateContext();
            var appointmentTypeRepo = new AppointmentTypeRepository(dbContext);
            //Act
            await appointmentTypeRepo.CreateAppointmentTypeAsync(appoType);
            await appointmentTypeRepo.SaveAsync();
            //Assert
            Assert.True(dbContext.AppointmentTypes.Count()>0);

        }


        [Fact]
        public async void AppointmentTypeRepository_DeleteAppointmentType()  // (AppointmentType appointmentType)
        {
            //Arrange
            var appoType = new AppointmentType
            {
                Id=800,
                Type = "Laser",
            };

            var dbContext = await Context.GenerateContext();
            var appointmentTypeRepo = new AppointmentTypeRepository(dbContext);
            //Act &Assert
            await appointmentTypeRepo.CreateAppointmentTypeAsync(appoType); //Count()==1
            await appointmentTypeRepo.SaveAsync();
            Assert.True(dbContext.AppointmentTypes.Count()>0);

            appointmentTypeRepo.DeleteAppointmentType(appoType); 
            await appointmentTypeRepo.SaveAsync();

            var appoTypeNull= await appointmentTypeRepo.GetAppointmentTypeAsync(800);
            Assert.Null(appoTypeNull);
        }


        [Fact]
        public async Task AppointmentTypeRepository_GetAppointmentTypeAsync()  //(int appointmentTypeId)
        {
            //Arrange
            var dbContext = await Context.Seed();
            var appointmentTypeRepo = new AppointmentTypeRepository(dbContext);
            //Act
            var result = await appointmentTypeRepo.GetAppointmentTypeAsync(1);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<AppointmentType>(result);
            Assert.Equal(result.Id, 1);

        }



        [Fact]
        public async Task AppointmentTypeRepository_GetAppointmentTypesAsync()
        {
            //Arrange
            var _db = await Context.Seed();  //Liefert ApplicationDbContext ohne Daten
            var appoTypeRepo = new AppointmentTypeRepository(_db);
            //Act
            var appointmentTypes = await appoTypeRepo.GetAppointmentTypesAsync();
            //Assert
            Assert.NotNull(appointmentTypes);
            Assert.IsType<List<AppointmentType>>(appointmentTypes);
            Assert.True(appointmentTypes.Count() > 0); 
        }


        [Fact]
        public async Task AppointmentTypeRepository_GetAppointmentTypeAsync_ShouldPreventSQLInjection()  //(int appointmentTypeId)
        {
            //Arrange
            var dbContext = await Context.Seed();
            var appointmentTypeRepo = new AppointmentTypeRepository(dbContext);
            var maliciousInput = "1; DROP TABLE AppointmentTypes; --"; 
            // Act & Assert
            await Assert.ThrowsAsync<FormatException>(() => appointmentTypeRepo.GetAppointmentTypeAsync(int.Parse(maliciousInput)));
        }



    }
}
