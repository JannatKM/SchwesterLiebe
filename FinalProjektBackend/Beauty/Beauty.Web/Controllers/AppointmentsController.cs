using AutoMapper;
using Beauty.Entity.Entities;
using Beauty.Repository.Contracts;
using Beauty.Shared.DTOs.Appointment;
using Microsoft.AspNetCore.Mvc;

namespace Beauty.Web.Controllers
{
    [Route("api/appointments")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentRepository _service;
        private readonly IDiscountRepository _discount;
        private readonly IProductRepository _product;
        private readonly IMapper _mapper;

        public AppointmentsController(IAppointmentRepository service,
            IMapper mapper,
            IDiscountRepository discount,
            IProductRepository product)
        {
            _service = service;
            _mapper = mapper;
            _discount = discount;
            _product = product;
        }

        [HttpGet]
        public async Task<IActionResult> GetAppointments()
        {
            try
            {
                var results = await _service.GetAppointmentsAsync();

                var resultsDto =
                    _mapper.Map<IEnumerable<AppointmentDto>>(results);

                return Ok(resultsDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("getEmployeeAppointments/{id}")]
        public async Task<IActionResult> GetEmployeeAppointments(int id)
        {
            try
            {
                var results = 
                    await _service.GetAppointmentsByEmployeeIdAsync(id);

                var resultsDto =
                    _mapper.Map<IEnumerable<AppointmentDto>>(results);

                return Ok(resultsDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("getLastThreeAppointments")]
        public async Task<IActionResult> GetLastThreeAppointments()
        {
            try
            {
                var results =
                    await _service.GetLastThreeAppointmentsAsync();

                var resultsDto =
                    _mapper.Map<IEnumerable<AppointmentDto>>(results);

                return Ok(resultsDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{modelId:int}", Name = "GetAppointmentById")]
        public async Task<IActionResult> GetAppointment(int modelId)
        {
            try
            {
                var result = await _service.GetAppointmentAsync(modelId);

                if (result is not null)
                {
                    var resultDto =
                        _mapper.Map<AppointmentDto>(result);

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
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentCreationDto model)
        {
            try
            {
                var discouts = await _discount.GetDiscountsAsync();

                foreach (var item in discouts)
                {
                    if (model.Date == item.StartDate || model.Date == item.EndDate)
                    {
                        var entity = new Appointment()
                        {
                            Date = model.Date,
                            StartTime = model.StartTime,
                            EndTime = model.EndTime,
                            EmployeeId = model.EmployeeId,
                            RoomId = model.RoomId,
                            AppointmentTypeId = model.AppointmentTypeId,
                            ProductId = model.ProductId,
                            DiscountId = item.Id
                        };

                        await _service.CreateAppointmentAsync(entity);
                        await _service.SaveAsync();

                        return StatusCode(201);
                    }
                    else
                    {
                        var entity = new Appointment()
                        {
                            Date = model.Date,
                            StartTime = model.StartTime,
                            EndTime = model.EndTime,
                            EmployeeId = model.EmployeeId,
                            RoomId = model.RoomId,
                            AppointmentTypeId = model.AppointmentTypeId,
                            ProductId = model.ProductId,
                            DiscountId = model.DiscountId
                        };

                        await _service.CreateAppointmentAsync(entity);
                        await _service.SaveAsync();

                        return StatusCode(201);
                    }
                }
                return BadRequest("The sent model is null.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{modelId:int}")]
        public async Task<IActionResult> UpdateAppointment(int modelId, [FromBody] AppointmentEditionDto model)
        {
            try
            {
                var editingModel = await _service.GetAppointmentAsync(modelId);

                if (editingModel is not null)
                {
                    editingModel.Date = model.Date;
                    editingModel.StartTime = model.StartTime;
                    editingModel.EndTime = model.EndTime;
                    editingModel.EmployeeId = model.EmployeeId;
                    editingModel.RoomId = model.RoomId;
                    editingModel.AppointmentTypeId = model.AppointmentTypeId;
                    editingModel.ProductId = model.ProductId;
                    editingModel.DiscountId = model.DiscountId;

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
        public async Task<IActionResult> DeleteAppointment(int modelId)
        {
            try
            {
                var deletingModel = 
                    await _service.GetAppointmentAsync(modelId);

                if (deletingModel is not null)
                {
                    _service.DeleteAppointment(deletingModel);
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
