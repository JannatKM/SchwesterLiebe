using AutoMapper;
using Beauty.Entity.Entities;
using Beauty.Repository.Contracts;
using Beauty.Shared.DTOs.Customer;
using Beauty.Shared.DTOs.Employee;
using Beauty.Shared.DTOs.User;
using Microsoft.AspNetCore.Mvc;

namespace Beauty.Web.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _service;
        private readonly IRoleRepository _roleRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository service,
            IRoleRepository roleRepository,
            ICustomerRepository customerRepository,
            IEmployeeRepository employeeRepository,
            IMapper mapper)
        {
            _service = service;
            _roleRepository = roleRepository;
            _customerRepository = customerRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var results =
                    await _employeeRepository.GetEmployeesAsync();

                var resultsDto =
                    _mapper.Map<IEnumerable<EmployeeDto>>(results);

                return Ok(resultsDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Route("getEmployee/{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            try
            {
                var result =
                    await _employeeRepository.GetEmployeeAsync(id);

                var resultDto =
                    _mapper.Map<EmployeeDto>(result);

                return Ok(resultDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var results = await _service.GetUsersAsync();

                var resultsDto = results.Select(x => new UserDto()
                {

                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Telephone = x.Telephone,
                    Password = x.Password,
                    RoleId = x.RoleId,

                }).ToList();

                return Ok(resultsDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{modelId:int}")]
        [Route("getSeperatedUser/{modelId}")]
        public async Task<IActionResult> GetUser(int modelId)
        {
            try
            {
                var result = await _service.GetUserAsync(modelId);

                if (result is not null)
                {
                    var resultDto = new UserDto()
                    {

                        Id = result.Id,
                        FirstName = result.FirstName,
                        LastName = result.LastName,
                        Email = result.Email,
                        Telephone = result.Telephone,
                        Password = result.Password,
                        RoleId = result.RoleId,

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


        [HttpGet("{modelId:int}")]
        [Route("getCurrentCustomer")]
        public async Task<IActionResult> GetCustomer(int modelId)
        {
            try
            {
                var result =
                    await _customerRepository.GetCustomerAsync(modelId);

                if (result is not null)
                {
                    var resultDto = new CustomerDto()
                    {

                        Id = result.Id,
                        UserId = result.UserId,

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
        [Route("login")]
        public async Task<IActionResult> Login(UserLoginDto login)
        {
            try
            {
                var userEntity = new User()
                {
                    Email = login.Email,
                    Password = login.Password,
                };

                var result =
                    await _service.GetUserByCredentialAsync(userEntity);

                if (result is not null)
                {
                    var customer =
                    await _customerRepository.GetCustomerByUserIdAsync(result.Id);

                    var employee =
                        await _employeeRepository.GetEmployeeByUserIdAsync(result.Id);

                    if (customer is not null)
                    {
                        var customerDto = new LogedInUserDto()
                        {

                            Id = customer.Id,
                            Email = result.Email,
                            RoleId = result.RoleId,
                            UserId = result.Id

                        };

                        return Ok(customerDto);
                    }
                    else if (employee is not null)
                    {
                        var employeeDto = new LogedInUserDto()
                        {

                            Id = employee.Id,
                            Email = result.Email,
                            RoleId = result.RoleId,
                            UserId = result.Id

                        };

                        return Ok(employeeDto);
                    }
                    else
                    {
                        var resultDto = new LogedInUserDto()
                        {

                            Id = result.Id,
                            Email = result.Email,
                            RoleId = result.RoleId,
                            UserId = result.Id

                        };

                        return Ok(resultDto);
                    }
                }

                return NotFound("There is no data based on that id.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> CreateUser([FromBody] UserCreationDto model)
        {
            try
            {
                if (model is not null)
                {
                    var entity = new User()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        Telephone = model.Telephone,
                        Password = model.Password,
                        RoleId = model.RoleId,
                    };

                    await _service.CreateUserAsync(entity);
                    await _service.SaveAsync();

                    var userRole = new UserRole()
                    {
                        UserId = entity.Id,
                        RoleId = entity.RoleId
                    };

                    await _roleRepository.CreateUserRoleAsync(userRole);
                    await _service.SaveAsync();

                    if (entity.RoleId == 2)
                    {
                        var customer = new Customer()
                        {
                            UserId = entity.Id
                        };

                        await _customerRepository.CreateCustomerAsync(customer);
                        await _service.SaveAsync();

                        var customerDto = new LogedInUserDto()
                        {
                            Id = customer.Id,
                            Email = entity.Email,
                            RoleId = entity.RoleId,
                            UserId = entity.Id
                        };

                        return StatusCode(201, customerDto);
                    }

                    if (entity.RoleId == 3)
                    {
                        var employee = new Employee()
                        {
                            UserId = entity.Id
                        };

                        await _employeeRepository.CreateEmployeeAsync(employee);
                        await _service.SaveAsync();

                        var employeeDto = new LogedInUserDto()
                        {
                            Id = employee.Id,
                            Email = entity.Email,
                            RoleId = entity.RoleId,
                            UserId = entity.Id
                        };

                        return StatusCode(201, employeeDto);
                    }
                }

                return BadRequest("The sent model is null.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("adminRegistration")]
        public async Task<IActionResult> AdminCreation([FromBody] UserCreationDto model)
        {
            try
            {
                if (model is not null)
                {
                    var entity = new User()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        Telephone = model.Telephone,
                        Password = model.Password,
                        RoleId = model.RoleId,
                    };

                    await _service.CreateUserAsync(entity);
                    await _service.SaveAsync();

                    var userRole = new UserRole()
                    {
                        UserId = entity.Id,
                        RoleId = entity.RoleId
                    };

                    await _roleRepository.CreateUserRoleAsync(userRole);
                    await _service.SaveAsync();

                    if (entity.RoleId == 3)
                    {
                        var employee = new Employee()
                        {
                            UserId = entity.Id
                        };

                        await _employeeRepository.CreateEmployeeAsync(employee);
                        await _service.SaveAsync();
                    }

                    var userDto = new UserDto()
                    {
                        Id = entity.Id,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        Email = entity.Email,
                        Telephone = entity.Telephone,
                        Password = entity.Password,
                        RoleId = entity.RoleId,
                    };

                    return StatusCode(201, userDto);

                }

                return BadRequest("The sent model is null.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{modelId:int}")]
        public async Task<IActionResult> UpdateUser(int modelId, [FromBody] UserEditionDto model)
        {
            try
            {
                var editingModel = await _service.GetUserAsync(modelId);

                if (editingModel is not null)
                {
                    editingModel.FirstName = model.FirstName;
                    editingModel.LastName = model.LastName;
                    editingModel.Email = model.Email;
                    editingModel.Telephone = model.Telephone;
                    editingModel.Password = model.Password;

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
        public async Task<IActionResult> DeleteUser(int modelId)
        {
            try
            {
                var deletingModel = await _service.GetUserAsync(modelId);

                if (deletingModel is not null)
                {
                    _service.DeleteUser(deletingModel);
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
