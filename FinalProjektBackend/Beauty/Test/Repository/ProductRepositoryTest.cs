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
    public class ProductRepositoryTest
    {
        private ProductRepository _productRepo;

        [Fact]
        public async Task ProductRepository_CreateProductAsync_DeleteProduct_GetProductAsync()
        {
            //Arrange
            var id = 333;
            var _db = await Context.GenerateContext();
            _productRepo = new ProductRepository(_db);
            var p = new Product
            {
                Id=id,
                Name = "Laser",
                Description = "Haarentfernung",
                Duration = 60,
                Price = 60
            };
            //Act 
           await _productRepo.CreateProductAsync(p);
           await _productRepo.SaveAsync();
           var product = await _productRepo.GetProductAsync(id);
           //Assert Create & Get
           Assert.NotNull(product);
           Assert.Equal(id, product.Id);

            //Act 
           _productRepo.DeleteProduct(p);
            await _productRepo.SaveAsync();
            var productNull = await _productRepo.GetProductAsync(id);
            //Assert Delete & Get
            Assert.Null(productNull);
        }  

        [Fact]
        public async Task ProductRepository_GetProductsAsync()
        {
            //Arrange
            var _db = await Context.Seed();
            _productRepo = new ProductRepository(_db);
            //Act
            var products=await _productRepo.GetProductsAsync();
            //Assert
            Assert.NotNull(products);
            Assert.IsType<List<Product>>(products);
            Assert.True(products.Count() > 0);

        }

        [Fact]
        public async Task ProductRepository_GetLastFourProductsAsync()
        {
            //Arrange
            var _db = await Context.Seed();
            _productRepo = new ProductRepository(_db);
            //Act
            var products = await _productRepo.GetLastFourProductsAsync();
            //Assert
            Assert.NotNull(products);
            Assert.IsType<List<Product>>(products);
            Assert.True(products.Count() ==4);

        }



        [Fact]
        public async Task ProductRepository_GetProductAsync_ShouldPreventSQLInjection()
        {
            //Arrange
            var _db = await Context.Seed();
            _productRepo = new ProductRepository(_db);
            var maliciousInput = "1; DROP TABLE Employee; --";
            //Act & assert
            await Assert.ThrowsAsync<FormatException>(() => _productRepo.GetProductAsync(int.Parse(maliciousInput)));
        }











        }
}
