using Beauty.Entity.Entities;
using Beauty.Repository.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Repository
{
    public class DiscountRepositoryTest
    {
        private DiscountRepository _discountRepo;

        [Fact]
        public async Task DiscountRepository_CreateDiscountAsync_DeleteDiscount_GetDiscountAsync()    //(Discount discount)
        {
            //Arrange
            var _db = await Context.GenerateContext();
            _discountRepo = new DiscountRepository(_db);
            //Act

            var disc = new Discount
            { 
                Id = 900,  //EfCore gibt eigentlich Id
                StartDate = "11:00",
                EndDate = "12:13",
                Percent = 11
            };
            //Act 
            await _discountRepo.CreateDiscountAsync(disc);
            await _discountRepo.SaveAsync();
            var discount=await _discountRepo.GetDiscountAsync(disc.Id);
            //Assert
            Assert.NotNull(discount);
            Assert.Equal(discount.Id,900);
            Assert.IsType<Discount>(discount);

            //Act für Delete
            _discountRepo.DeleteDiscount(disc);
            await _discountRepo.SaveAsync();
            var discountNull = await _discountRepo.GetDiscountAsync(disc.Id);
            //Assert
            Assert.Null(discountNull);
        }

        [Fact]
        public async Task DiscountRepository_GetDiscountsAsync()
        {
            //Arrange
            var _db = await Context.Seed();
            _discountRepo = new DiscountRepository(_db);
            //Act
            var discounts= await _discountRepo.GetDiscountsAsync();
            //Assert
            Assert.NotNull(discounts);
            Assert.IsType<List<Discount>>(discounts);

        }



        [Fact]
        public async Task DiscountRepository_GetDiscountAsync_ShouldPreventSQLInjection() //Beispiel Sql-Injection
        {
            // Arrange
            var _db = await Context.Seed();
            _discountRepo = new DiscountRepository(_db);
            var maliciousInput = "1; DROP TABLE Discounts; --";
            //Act & assert
            await Assert.ThrowsAsync<FormatException>(() => _discountRepo.GetDiscountAsync(int.Parse(maliciousInput)));
        }















    }
}
