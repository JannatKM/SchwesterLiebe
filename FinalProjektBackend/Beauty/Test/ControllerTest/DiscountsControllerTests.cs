using Beauty.Entity.Entities;
using Beauty.Repository.Contracts;
using Beauty.Shared.DTOs.Discount;
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
    public class DiscountsControllerTests
    {
        private readonly Mock<IDiscountRepository> _discountRepo;

        public DiscountsControllerTests()
        {
            _discountRepo = new Mock<IDiscountRepository>();
        }

        [Fact]
        public async Task DiscountsController_GetDiscounts()
        {
            //Arrange
            var discounts = Enumerable.Range(1, 2).Select(x => new Discount
            {
                Id = x,
                Percent = 20 + x,
                StartDate = "06-05-2024",
                EndDate = "25-05-2024",
            });

            _discountRepo.Setup(x => x.GetDiscountsAsync()).Returns(Task.FromResult(discounts));
            var controller = new DiscountsController(_discountRepo.Object);

            //act
            IActionResult actionResult = await controller.GetDiscounts();
            var OkResult = actionResult as OkObjectResult;
            var discountsDtos = OkResult.Value as IEnumerable<DiscountDto>;
            //Assert
            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
            Assert.Equal(2, discountsDtos.Count());

        }


        [Fact]
        public async Task DiscountsController_GetDiscount() //(int modelId)
        {
            var modelId = 1;
            var discount = new Discount
            {
                Id = 1,
                Percent = 25,
                StartDate = "06-05-2024",
                EndDate = "25-05-2024",
            };

            _discountRepo.Setup(x => x.GetDiscountAsync(modelId)).Returns(Task.FromResult(discount));

            var controller = new DiscountsController(_discountRepo.Object);

            //act
            IActionResult actionResult = await controller.GetDiscount(modelId);
            var OkResult = actionResult as OkObjectResult;
            var d = OkResult.Value as DiscountDto;
            //Assert
            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
            Assert.Equal(1, d.Id);
        }



        [Fact]
        public async Task DiscountsController_CreateDiscount() //([FromBody] DiscountCreationDto model)
        {

            var model = new DiscountCreationDto
            {
                Percent = 25,
                StartDate = "06-05-2024",
                EndDate = "25-05-2024"
            };

            _discountRepo.Setup(x => x.CreateDiscountAsync(It.IsAny<Discount>())).Returns(Task.CompletedTask);
            _discountRepo.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);

            var controller = new DiscountsController(_discountRepo.Object);
            //act
            IActionResult actionResult = await controller.CreateDiscount(model);
            var statusCodeResult = Assert.IsType<StatusCodeResult>(actionResult);

            //Assert
            Assert.NotNull(actionResult);
            Assert.Equal(201, statusCodeResult.StatusCode);
        }


        [Fact]
        public async Task DiscountsController_UpdateDiscount() //(int modelId, [FromBody] DiscountEditionDto model)
        {
            //Arrange
            var discount = new Discount
            {
                Id = 1,
                Percent = 25,
                StartDate = "06-05-2024",
                EndDate = "25-05-2024",
            };
            var model = new DiscountEditionDto
            {
                Id = 1,
                Percent = 30,
                StartDate = "06-05-2024",
                EndDate = "25-05-2024",
            };

            _discountRepo.Setup(x => x.GetDiscountAsync(It.IsAny<int>())).Returns(Task.FromResult(discount));
            _discountRepo.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);
            var controller = new DiscountsController(_discountRepo.Object);

            //Act
            IActionResult actionResult = await controller.UpdateDiscount(1, model);
            var OkResult = actionResult as OkObjectResult;
            var value = OkResult.Value as string;
            //Assert
            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
            Assert.Equal(value, "Edition done.");

        }

        [Fact]
        public async Task DeleteDiscount()  //(int modelId)
        {
            //Arrange
            var discount = new Discount
            {
                Id = 2,
                Percent = 25,
                StartDate = "06-05-2024",
                EndDate = "25-05-2024",
            };
            _discountRepo.Setup(x => x.GetDiscountAsync(It.IsAny<int>())).Returns(Task.FromResult(discount));
            _discountRepo.Setup(x => x.DeleteDiscount(It.IsAny<Discount>()));
            _discountRepo.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);
            var controller = new DiscountsController(_discountRepo.Object);

            //Act
            IActionResult actionResult = await controller.DeleteDiscount(2);
            var OkResult = actionResult as OkObjectResult;
            var value = OkResult.Value as string;
            //Assert
            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
            Assert.Equal(value, "Deletion done.");
        }
    }
}
