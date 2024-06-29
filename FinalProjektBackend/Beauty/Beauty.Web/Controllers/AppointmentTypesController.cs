using Beauty.Entity.Entities;
using Beauty.Repository.Contracts;
using Beauty.Shared.DTOs.AppointmentType;
using Beauty.Shared.DTOs.Product;
using Beauty.Shared.DTOs.Room;
using Beauty.Shared.DTOs.User;
using Microsoft.AspNetCore.Mvc;

namespace Beauty.Web.Controllers
{
    [Route("api/appointmentTypes")]
    [ApiController]
    public class AppointmentTypesController : ControllerBase
    {
        private readonly IAppointmentTypeRepository _service;

        public AppointmentTypesController(IAppointmentTypeRepository service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAppointmentTypes()
        {
            try
            {
                var results = await _service.GetAppointmentTypesAsync();

                var resultsDto = results.Select(x => new AppointmentTypeDto()
                {

                    Id = x.Id,
                    Type = x.Type,

                }).ToList();

                return Ok(resultsDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{modelId:int}", Name = "GetAppointmentTypeById")]
        public async Task<IActionResult> GetAppointmentType(int modelId)
        {
            try
            {
                var result = await _service.GetAppointmentTypeAsync(modelId);

                if (result is not null)
                {
                    var resultDto = new AppointmentTypeDto()
                    {

                        Id = result.Id,
                        Type = result.Type,

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
        public async Task<IActionResult> CreateAppointmentType([FromBody] AppointmentTypeCreationDto model)
        {
            try
            {
                if (model is not null)
                {
                    var entity = new AppointmentType()
                    {
                        Type = model.Type,
                    };

                    await _service.CreateAppointmentTypeAsync(entity);
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
        public async Task<IActionResult> UpdateAppointmentType(int modelId, [FromBody] AppointmentTypeEditionDto model)
        {
            try
            {
                var editingModel = await _service.GetAppointmentTypeAsync(modelId);

                if (editingModel is not null)
                {
                    editingModel.Type = model.Type;

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
        public async Task<IActionResult> DeleteAppointmentType(int modelId)
        {
            try
            {
                var deletingModel = await _service.GetAppointmentTypeAsync(modelId);

                if (deletingModel is not null)
                {
                    _service.DeleteAppointmentType(deletingModel);
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
