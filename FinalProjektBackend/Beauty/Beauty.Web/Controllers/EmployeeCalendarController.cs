using AutoMapper;
using Beauty.Entity.Entities;
using Beauty.Repository.Contracts;
using Beauty.Shared.DTOs.EmployeeCalendar;
using Beauty.Shared.DTOs.EmployeeTime;
using Microsoft.AspNetCore.Mvc;

namespace Beauty.Web.Controllers
{
    [Route("api/calendar")]
    [ApiController]
    public class EmployeeCalendarController : ControllerBase
    {
        private readonly IEmployeeCalendarRepository _service;
        private readonly IEmployeeTimeRepository _timeService;
        private readonly IMapper _mapper;

        public EmployeeCalendarController(IEmployeeCalendarRepository service,
            IMapper mapper,
            IEmployeeTimeRepository timeService)
        {
            _service = service;
            _mapper = mapper;
            _timeService = timeService;
        }

        [HttpGet]
        [Route("getTimes/{date}")]
        public async Task<IActionResult> GetEmployeeTimes(string date)
        {
            try
            {
                var results =
                    await _timeService.GetEmployeeTimesByDateAsync(date);

                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getDateTimes/{id}/{date}")]
        public async Task<IActionResult> GetEmployeeTimes(int id, string date)
        {
            try
            {
                var results =
                    await _timeService.GetEmployeeTimesByDateAsync(id, date);

                if (results.Count() > 0)
                {
                    return Ok(results);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeCalendars()
        {
            try
            {
                var results =
                    await _service.GetEmployeeCalendarsAsync();

                var resultsDto =
                    _mapper.Map<IEnumerable<EmployeeCalendarDto>>(results);

                return Ok(resultsDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getDates/{id}")]
        public async Task<IActionResult> GetEmployeeTimesDate(int id)
        {
            try
            {
                var results =
                    await _service.GetDateByEmployeeIdAsync(id);

                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("getTimesByDate/{date}")]
        public async Task<IActionResult> GetEmployeeTimesByDate(string date)
        {
            try
            {
                var results =
                    await _timeService.GetEmployeeTimesByDateAsync(date);

                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("getEmployeeCalendars/{id}")]
        public async Task<IActionResult> GetEmployeeCalendars(int id)
        {
            try
            {
                var results =
                    await _service.GetEmployeeCalendarsByEmployeeIdAsync(id);

                var resultsDto =
                    _mapper.Map<IEnumerable<EmployeeCalendarDto>>(results);

                return Ok(resultsDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{modelId:int}", Name = "GetEmployeeCalendarById")]
        public async Task<IActionResult> GetEmployeeCalendar(int modelId)
        {
            try
            {
                var result =
                    await _timeService.GetEmployeeTimeByIdAsync(modelId);

                if (result is not null)
                {
                    var resultDto =
                        _mapper.Map<EmployeeTimeDto>(result);

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
        public async Task<IActionResult> CreateEmployeeCalendar(
            [FromBody] EmployeeCalendarForCreationDto model)
        {
            try
            {
                var entity = _mapper.Map<EmployeeCalendar>(model);

                await _service.CreateEmployeeCalendarAsync(entity);
                await _service.SaveAsync();

                if (!entity.IsVacation)
                {
                    for (int i = 9; i < 17; i++)
                    {
                        var employeeTime = new EmployeeTime()
                        {
                            Date = entity.Date,
                            EmployeeId = entity.EmployeeId,
                            Time = i + " : 00"
                        };

                        await _timeService.CreateEmployeeTimeAsync(employeeTime);
                    }
                    await _timeService.SaveAsync();
                }

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{modelId:int}")]
        public async Task<IActionResult> UpdateEmployeeCalendar(int modelId,
            [FromBody] EmployeeCalendarForEditionDto model)
        {
            try
            {
                var entity =
                    await _service.GetEmployeeCalendarAsync(modelId);

                _mapper.Map(model, entity);

                await _service.SaveAsync();

                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{modelId:int}")]
        public async Task<IActionResult> DeleteEmployeeCalendar(int modelId)
        {
            try
            {
                var deletingModel =
                    await _service.GetEmployeeCalendarAsync(modelId);

                if (deletingModel is not null)
                {
                    _service.DeleteEmployeeCalendar(deletingModel);
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
