using AutoMapper;
using Beauty.Entity.Entities;
using Beauty.Repository.Contracts;
using Beauty.Shared.DTOs.Room;
using Microsoft.AspNetCore.Mvc;

namespace Beauty.Web.Controllers
{
    [Route("api/rooms")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomRepository _service;
        private readonly IMapper _mapper;

        public RoomsController(IRoomRepository service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetRooms()
        {
            try
            {
                var results = await _service.GetRoomsAsync();

                var resultsDto = results.Select(x => new RoomDto()
                {

                    Id = x.Id,
                    Name = x.Name

                }).ToList();

                return Ok(resultsDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{modelId:int}", Name = "GetRoomById")]
        public async Task<IActionResult> GetRoom(int modelId)
        {
            try
            {
                var result = await _service.GetRoomAsync(modelId);

                if (result is not null)
                {
                    var resultDto = new RoomDto()
                    {
                        Id = result.Id,
                        Name = result.Name,
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
        public async Task<IActionResult> CreateRoom([FromBody] RoomCreationDto model)
        {
            try
            {
                if (model is not null)
                {
                    var entity = 
                        _mapper.Map<Room>(model);

                    await _service.CreateRoomAsync(entity);
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
        public async Task<IActionResult> UpdateRoom(int modelId, [FromBody] RoomEditionDto model)
        {
            try
            {
                var editingModel = await _service.GetRoomAsync(modelId);

                if (editingModel is not null)
                {
                    editingModel.Name = model.Name;
                    editingModel.IsDown = model.IsDown;
                    editingModel.Description = model.Description;

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
        public async Task<IActionResult> DeleteRoom(int modelId)
        {
            try
            {
                var deletingModel = await _service.GetRoomAsync(modelId);

                if (deletingModel is not null)
                {
                    _service.DeleteRoom(deletingModel);
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
