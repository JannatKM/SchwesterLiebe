using AutoMapper;
using Beauty.Entity.Entities;
using Beauty.Repository.Contracts;
using Beauty.Shared.DTOs.Booking;
using Microsoft.AspNetCore.Mvc;

namespace Beauty.Web.Controllers
{
    [Route("api/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingRepository _service;
        private readonly IAppointmentRepository _serviceAppointment;
        private readonly IEmployeeTimeRepository _serviceTime;
        private readonly IMapper _mapper;

        public BookingsController(IBookingRepository service,
            IMapper mapper,
            IAppointmentRepository serviceAppointment,
            IEmployeeTimeRepository serviceTime)
        {
            _service = service;
            _mapper = mapper;
            _serviceAppointment = serviceAppointment;
            _serviceTime = serviceTime;
        }

        [HttpGet]
        public async Task<IActionResult> GetBookings()
        {
            try
            {
                var results = await _service.GetBookingsAsync();

                var resultsDto = 
                    _mapper.Map<IEnumerable<BookingDto>>(results);

                return Ok(resultsDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("getByUserId/{userId}")]
        [HttpGet("{userId:int}")]
        public async Task<IActionResult> GetBookingsByUserId(int userId)
        {
            try
            {
                var results = 
                    await _service.GetBookingsByUserIdAsync(userId);

                var resultsDto =
                    _mapper.Map<IEnumerable<BookingDto>>(results);

                return Ok(resultsDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{employeeId:int}/{date}/{time}")]
        [Route("getBookingDetails/{employeeId}/{date}/{time}")]
        public async Task<IActionResult> GetBooking(int employeeId, string date, string time)
        {
            try
            {
                var result = 
                    await _service.GetBookingAsync(employeeId, date, time);

                if (result is not null)
                {
                    var resultDto = _mapper.Map<BookingDto>(result);

                    return Ok(resultDto);
                }

                return NotFound("There is no data based on that id.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{modelId:int}", Name = "GetBookingById")]
        public async Task<IActionResult> GetBooking(int modelId)
        {
            try
            {
                var result = await _service.GetBookingAsync(modelId);

                if (result is not null)
                {
                    var resultDto = _mapper.Map<BookingDto>(result);

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
        public async Task<IActionResult> CreateBooking([FromBody] BookingCreationDto model)
        {
            try
            {
                if (model is not null)
                {
                    var entity = _mapper.Map<Booking>(model);

                    await _service.CreateBookingAsync(entity);
                    await _service.SaveAsync();

                    var employeeTime =
                        await _serviceTime.GetEmployeeTimeByIdAsync(model.EmployeeTimeId);

                    employeeTime.IsReserved = true;

                    await _serviceTime.SaveAsync();

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
        public async Task<IActionResult> UpdateBooking(int modelId, [FromBody] BookingEditionDto model)
        {
            try
            {
                var editingModel = await _service.GetBookingAsync(modelId);

                if (editingModel is not null)
                {
                    editingModel.EmployeeId = model.EmployeeId;
                    editingModel.CustomerId = model.CustomerId;
                    //editingModel.AppointmentId = model.AppointmentId;
                    editingModel.ProductId = model.ProductId;
                    //editingModel.DiscountId = model.DiscountId;
                    editingModel.RoomId = model.RoomId;

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
        public async Task<IActionResult> DeleteBooking(int modelId)
        {
            try
            {
                var deletingModel = await _service.GetBookingAsync(modelId);

                if (deletingModel is not null)
                {
                    _service.DeleteBooking(deletingModel);
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
