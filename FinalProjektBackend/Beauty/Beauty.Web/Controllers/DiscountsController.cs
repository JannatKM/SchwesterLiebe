using Beauty.Entity.Entities;
using Beauty.Repository.Contracts;
using Beauty.Shared.DTOs.Discount;
using Microsoft.AspNetCore.Mvc;

namespace Beauty.Web.Controllers
{
    [Route("api/discounts")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscountRepository _service;

        public DiscountsController(IDiscountRepository service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetDiscounts()
        {
            try
            {
                var results = await _service.GetDiscountsAsync();

                var resultsDto = results.Select(x => new DiscountDto()
                {

                    Id = x.Id,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    Percent = x.Percent,

                }).ToList();

                return Ok(resultsDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{modelId:int}", Name = "GetDiscountById")]
        public async Task<IActionResult> GetDiscount(int modelId)
        {
            try
            {
                var result = await _service.GetDiscountAsync(modelId);

                if (result is not null)
                {
                    var resultDto = new DiscountDto()
                    {

                        Id = result.Id,
                        StartDate = result.StartDate,
                        EndDate = result.EndDate,
                        Percent = result.Percent,

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
        public async Task<IActionResult> CreateDiscount([FromBody] DiscountCreationDto model)
        {
            try
            {
                if (model is not null)
                {
                    var entity = new Discount()
                    {
                        StartDate = model.StartDate,
                        EndDate = model.EndDate,
                        Percent = model.Percent,
                    };

                    await _service.CreateDiscountAsync(entity);
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
        public async Task<IActionResult> UpdateDiscount(int modelId, [FromBody] DiscountEditionDto model)
        {
            try
            {
                var editingModel = await _service.GetDiscountAsync(modelId);

                if (editingModel is not null)
                {
                    editingModel.StartDate = model.StartDate;
                    editingModel.EndDate = model.EndDate;
                    editingModel.Percent = model.Percent;

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
        public async Task<IActionResult> DeleteDiscount(int modelId)
        {
            try
            {
                var deletingModel = await _service.GetDiscountAsync(modelId);

                if (deletingModel is not null)
                {
                    _service.DeleteDiscount(deletingModel);
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
