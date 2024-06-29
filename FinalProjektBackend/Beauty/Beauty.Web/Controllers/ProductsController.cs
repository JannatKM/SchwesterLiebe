using Beauty.Entity.Entities;
using Beauty.Repository.Contracts;
using Beauty.Shared.DTOs.Product;
using Microsoft.AspNetCore.Mvc;

namespace Beauty.Web.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _service;

        public ProductsController(IProductRepository service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var results = await _service.GetProductsAsync();

                var resultsDto = results.Select(x => new ProductDto()
                {

                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Duration = x.Duration,
                    Price = x.Price

                }).ToList();

                return Ok(resultsDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getLastFourProducts")]
        public async Task<IActionResult> GetLastFourProducts()
        {
            try
            {
                var results = await _service.GetLastFourProductsAsync();

                var resultsDto = results.Select(x => new ProductDto()
                {

                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Duration = x.Duration,
                    Price = x.Price

                }).ToList();

                return Ok(resultsDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{modelId:int}", Name = "GetProductById")]
        public async Task<IActionResult> GetProduct(int modelId)
        {
            try
            {
                var result = await _service.GetProductAsync(modelId);

                if (result is not null)
                {
                    var resultDto = new ProductDto()
                    {

                        Id = result.Id,
                        Name = result.Name,
                        Description = result.Description,
                        Duration = result.Duration,
                        Price = result.Price

                    };

                    return Ok(resultDto);
                }

                return NotFound("There is no data based on that id.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreationDto model)
        {
            try
            {
                if (model is not null)
                {
                    var entity = new Product()
                    {
                        Name = model.Name,
                        Description = model.Description,
                        Price = model.Price,
                        Duration = model.Duration,
                    };

                    await _service.CreateProductAsync(entity);
                    await _service.SaveAsync();

                    return StatusCode(201);
                }

                return BadRequest("The sent model is null.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{modelId:int}")]
        public async Task<IActionResult> UpdateProduct(int modelId, [FromBody] ProductEditionDto model)
        {
            try
            {
                var editingModel = await _service.GetProductAsync(modelId);

                if (editingModel is not null)
                {
                    editingModel.Name = model.Name;
                    editingModel.Description = model.Description;
                    editingModel.Duration = model.Duration;
                    editingModel.Price = model.Price;

                    await _service.SaveAsync();

                    return Ok("Edition done.");
                }

                return BadRequest("The sent model is null.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{modelId:int}")]
        public async Task<IActionResult> DeleteProduct(int modelId)
        {
            try
            {
                var deletingModel = await _service.GetProductAsync(modelId);

                if (deletingModel is not null)
                {
                    _service.DeleteProduct(deletingModel);
                    await _service.SaveAsync();

                    return Ok("Deletion done.");
                }

                return BadRequest("The sent model is null.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
