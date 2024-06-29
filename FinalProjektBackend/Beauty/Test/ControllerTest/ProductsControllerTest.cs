using Beauty.Entity.Entities;
using Beauty.Repository.Contracts;
using Beauty.Shared.DTOs.Product;
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
    public class ProductsControllerTest
    {

        private readonly Mock<IProductRepository> _productRepo;

        public ProductsControllerTest()
        {
            _productRepo = new Mock<IProductRepository>();
        }



        [Fact]
        public async Task ProductsController_GetProducts()
        {
            var products = Enumerable.Range(1, 2).Select(x => new Product
            {
                Id = x,
                Name = "Laser",
                Price = 200,
                Duration = 1,

            });

            _productRepo.Setup(x => x.GetProductsAsync()).Returns(Task.FromResult(products));
            var controller = new ProductsController(_productRepo.Object);
            //Act
            IActionResult actionResult = await controller.GetProducts();
            var OkResult = actionResult as OkObjectResult;
            var productDtos = OkResult.Value as IEnumerable<ProductDto>;
            //Assert
            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
            Assert.True(productDtos.Count()>0);

            //return Ok(resultsDto);
        }


        [Fact]
        public async Task ProductsController_GetLastFourProducts()
        {
            var products = Enumerable.Range(1, 4).Select(x => new Product
            {
                Id = x,
                Name = "Laser",
                Price = 200,
                Duration = 1,
            });
            _productRepo.Setup(x => x.GetLastFourProductsAsync()).Returns(Task.FromResult(products));
            var controller = new ProductsController(_productRepo.Object);
            //Act
            IActionResult actionResult = await controller.GetLastFourProducts();
            var OkResult = actionResult as OkObjectResult;
            var productDtos = OkResult.Value as IEnumerable<ProductDto>;
            //Assert
            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
            Assert.Equal(4, productDtos.Count());
        }

        [Fact]
        public async Task ProductsController_GetProduct()  //(int modelId)
        {
            var p = new Product
            {
                Id = 1,
                Name = "Waxing",
                Price = 200,
                Duration = 2,
            };
            _productRepo.Setup(x => x.GetProductAsync(It.IsAny<int>())).Returns(Task.FromResult(p));
            var controller = new ProductsController(_productRepo.Object);
            //Act
            IActionResult actionResult = await controller.GetProduct(1);
            var OkResult = actionResult as OkObjectResult;
            var pDto = OkResult.Value as ProductDto;
            //Assert
            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
            Assert.Equal(1, pDto.Id);
        }

        [Fact]
        public async Task ProductsController_CreateProduct() 
        {
            var model = new ProductCreationDto
            {
                Name = "Waxing",
                Description = "Haarentfernug",
                Price = 200,
                Duration = 2
            };

            _productRepo.Setup(x => x.CreateProductAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);
            _productRepo.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);
            var controller = new ProductsController(_productRepo.Object);
            //Act
            IActionResult actionResult = await controller.CreateProduct(model);
            var statusCodeResult = Assert.IsType<StatusCodeResult>(actionResult);

            //Assert
            Assert.NotNull(actionResult);
            Assert.Equal(201, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task ProductsController_UpdateProduct()  //(int modelId, [FromBody] ProductEditionDto model)
        {
            var p = new Product
            {
                Id = 1,
                Name = "Waxing",
                Price = 200,
                Duration = 2
            };
            var model = new ProductEditionDto
            {
                Id = 1,
                Name = "Waxing",
                Description = "Haarentfernug",
                Price = 250,
                Duration = 2
            };

            _productRepo.Setup(x => x.GetProductAsync(It.IsAny<int>())).Returns(Task.FromResult(p));
            _productRepo.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);
            var controller = new ProductsController(_productRepo.Object);
            //Act
            IActionResult actionResult = await controller.UpdateProduct(It.IsAny<int>(), model);
            var OkResult = actionResult as OkObjectResult;
            var value = OkResult.Value as string;
            //Assert
            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
            Assert.Equal("Edition done.", value);
        }

        [Fact]
        public async Task ProductsController_DeleteProduct()  //(int modelId)
        {
            var p = new Product
            {
                Id = 3,
                Name = "Waxing",
                Price = 200,
                Duration = 2,

            };
            _productRepo.Setup(x => x.GetProductAsync(It.IsAny<int>())).Returns(Task.FromResult(p));
            _productRepo.Setup(x => x.DeleteProduct(It.IsAny<Product>()));
            _productRepo.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);

            var controller = new ProductsController(_productRepo.Object);
            //Act
            IActionResult actionResult = await controller.DeleteProduct(It.IsAny<int>());
            var OkResult = actionResult as OkObjectResult;
            var value = OkResult.Value as string;
            //Assert
            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
            Assert.Equal("Deletion done.", value);
        }





    }
}
