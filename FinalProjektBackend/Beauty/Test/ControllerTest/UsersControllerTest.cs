using AutoMapper;
using Beauty.Entity.Entities;
using Beauty.Repository.Contracts;
using Beauty.Repository.Services;
using Beauty.Shared.DTOs.Customer;
using Beauty.Shared.DTOs.Employee;
using Beauty.Shared.DTOs.User;
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
    public class UsersControllerTest
    {


        private readonly Mock<IUserRepository> _userRepo;
        private readonly Mock<IRoleRepository> _roleRepo;
        private readonly Mock<ICustomerRepository> _customerRepo;
        private readonly Mock<IEmployeeRepository> _employeeRepo;
        private readonly Mock<IMapper> _mapper;

        public UsersControllerTest()
        {
            _userRepo = new Mock<IUserRepository>();
            _roleRepo = new Mock<IRoleRepository>();
            _customerRepo = new Mock<ICustomerRepository>();
            _employeeRepo = new Mock<IEmployeeRepository>();
            _mapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task UsersController_GetEmployees()
        {
            var employees = Enumerable.Range(1, 2).Select(x => new Employee
            {
                Id = x,
                UserId = x
            });
            var employeeDtos = Enumerable.Range(1, 2).Select(x => new EmployeeDto
            {
                Id = x,
                UserId = x
            });

            _employeeRepo.Setup(x => x.GetEmployeesAsync()).Returns(Task.FromResult(employees));
            _mapper.Setup(x => x.Map<IEnumerable<EmployeeDto>>(employees)).Returns(employeeDtos);
            var controller = new UsersController(_userRepo.Object, _roleRepo.Object, _customerRepo.Object, _employeeRepo.Object, _mapper.Object);
            //Act
            IActionResult actionResult = await controller.GetEmployees();
            var OkResult = actionResult as OkObjectResult;
            var empDtos = OkResult.Value as IEnumerable<EmployeeDto>;
            //Assert
            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
            Assert.True(empDtos.Count()>0);

        }

        [Fact]
        public async Task UsersController_GetEmployee()  //(int id)
        {
            //Arrange
            var emp = new Employee
            {
                Id = 1,
                UserId = 101,
            };
            var empDto = new EmployeeDto
            {
                Id = 1,
                UserId = 101,
            };

            _employeeRepo.Setup(x => x.GetEmployeeAsync(It.IsAny<int>())).Returns(Task.FromResult(emp));
            _mapper.Setup(x => x.Map<EmployeeDto>(emp)).Returns(empDto);
            var controller = new UsersController(_userRepo.Object, _roleRepo.Object, _customerRepo.Object, _employeeRepo.Object, _mapper.Object);
            //Act
            IActionResult actionResult = await controller.GetEmployee(1);
            var OkResult = actionResult as OkObjectResult;
            var value = OkResult.Value as EmployeeDto;
            //Assert
            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
        }







        [Fact]
        public async Task UsersController_GetUsers()
        {
            var users = Enumerable.Range(1, 2).Select(x => new User
            {
                Id = x,
                FirstName = "User" + x,
                LastName="Lastname"+x,
                Password = "TestPasswort123",
                Email = "user" + x + "@gmail.com",
                Telephone="+1234567"+x,
                RoleId = x
            });

            _userRepo.Setup(x => x.GetUsersAsync()).Returns(Task.FromResult(users));
            var controller = new UsersController(_userRepo.Object, _roleRepo.Object, _customerRepo.Object, _employeeRepo.Object, _mapper.Object);
            //Act
            IActionResult actionResult = await controller.GetUsers();
            var OkResult = actionResult as OkObjectResult;
            var userDtos = OkResult.Value as IEnumerable<UserDto>;
            //Assert
            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
            Assert.True(userDtos.Count() > 0);

        }       //return Ok(resultsDto);

        [Fact]
        public async Task UsersController_GetUser()  //(int modelId)
        {

            var user = new User
            {
                Id = 1,
                FirstName = "User1",
                LastName="userLastname",
                Password = "TestPasswort123",
                Email = "user1@gmail.com",
                Telephone="+1234567",
                RoleId = 1
            };

            _userRepo.Setup(x => x.GetUserAsync(It.IsAny<int>())).Returns(Task.FromResult(user));
            var controller = new UsersController(_userRepo.Object, _roleRepo.Object, _customerRepo.Object, _employeeRepo.Object, _mapper.Object);
            //Act
            IActionResult actionResult = await controller.GetUser(1);
            var OkResult = actionResult as OkObjectResult;
            var userDto = OkResult.Value as UserDto;
            //Assert
            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
            Assert.Equal(1, userDto.Id);
        }

        [Fact]
        public async Task UsersController_GetCustomer() //(int modelId)
        {
            var cust = new Customer
            {
                Id = 1,
                UserId = 1,
            };

            _customerRepo.Setup(x => x.GetCustomerAsync(It.IsAny<int>())).Returns(Task.FromResult(cust));
            var controller = new UsersController(_userRepo.Object, _roleRepo.Object, _customerRepo.Object, _employeeRepo.Object, _mapper.Object);
            //Act
            IActionResult actionResult = await controller.GetCustomer(1);
            var OkResult = actionResult as OkObjectResult;
            var value = OkResult.Value as CustomerDto;
            //Assert
            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
            Assert.Equal(1, value.Id);

        }


        [Fact]
        public async Task UsersController_Login() //(UserLoginDto login)
        {

            var login = new UserLoginDto
            {
                Email = "user12345@gmail.com",
                Password = "TestPasswort123"
            };

            var user = new User()
            {
                Id = 1,
                FirstName="Max",
                LastName="Müller",
                Email = login.Email,
                Password = login.Password,
                RoleId = 2,
                Telephone="+1234567",

            };
            _userRepo.Setup(x => x.GetUserByCredentialAsync(It.IsAny<User>())).Returns(Task.FromResult(user));

            var cust = new Customer
            {
                Id = 1,
                UserId = 1,

            };
            _customerRepo.Setup(x => x.GetCustomerByUserIdAsync(user.Id)).Returns(Task.FromResult(cust));
            _employeeRepo.Setup(x => x.GetEmployeeByUserIdAsync(user.Id)).Returns(Task.FromResult((Employee)null));

            var controller = new UsersController(_userRepo.Object, _roleRepo.Object, _customerRepo.Object, _employeeRepo.Object, _mapper.Object);
            //Act
            IActionResult actionResult = await controller.Login(login);
            var OkResult = actionResult as OkObjectResult;
            var value = OkResult.Value as LogedInUserDto;
            //Assert
            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
            Assert.Equal(1, value.Id);

        }


        [Fact]
        public async Task UsersController_CreateUser() //([FromBody] UserCreationDto model)
        {
            var model = new UserCreationDto()
            {
                FirstName = "Max",
                LastName = "Müller",
                Email = "test@gmail.com",
                Password = "password12345",
                RoleId = 2,
                Telephone = "+12345567"
            };


            _userRepo.Setup(x => x.CreateUserAsync(It.IsAny<User>())).Returns(Task.CompletedTask);
            _userRepo.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);
            _roleRepo.Setup(x => x.CreateUserRoleAsync(It.IsAny<UserRole>())).Returns(Task.CompletedTask);
            _roleRepo.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);
            _customerRepo.Setup(x => x.CreateCustomerAsync(It.IsAny<Customer>())).Returns(Task.CompletedTask);
            _customerRepo.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);
            var controller = new UsersController(_userRepo.Object, _roleRepo.Object, _customerRepo.Object, _employeeRepo.Object, _mapper.Object);

            //Act
            IActionResult actionResult = await controller.CreateUser(model);
            var statusCodeResult = Assert.IsType<ObjectResult>(actionResult);

            //Assert
            Assert.NotNull(actionResult);
            Assert.Equal(201, statusCodeResult.StatusCode);

        }

        [Fact]
        public async Task UsersController_AdminCreation()  //([FromBody] UserCreationDto model)
        {
            var model = new UserCreationDto()
            {
                FirstName = "Max",
                LastName = "Müller",
                Email = "test@gmail.com",
                Password = "password12345",
                RoleId = 3,
                Telephone = "+12345567"
            };

            _userRepo.Setup(x => x.CreateUserAsync(It.IsAny<User>())).Returns(Task.CompletedTask);
            _userRepo.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);
            _roleRepo.Setup(x => x.CreateUserRoleAsync(It.IsAny<UserRole>())).Returns(Task.CompletedTask);
            _roleRepo.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);
            _employeeRepo.Setup(x => x.CreateEmployeeAsync(It.IsAny<Employee>())).Returns(Task.CompletedTask);
            _employeeRepo.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);
            var controller = new UsersController(_userRepo.Object, _roleRepo.Object, _customerRepo.Object, _employeeRepo.Object, _mapper.Object);

            //Act
            IActionResult actionResult = await controller.AdminCreation(model);
            var statusCodeResult = Assert.IsType<ObjectResult>(actionResult);

            //Assert
            Assert.NotNull(actionResult);
            Assert.Equal(201, statusCodeResult.StatusCode);
            // return StatusCode(201, userDto);

        }


        [Fact]
        public async Task UsersController_UpdateUser()  //(int modelId, [FromBody] UserEditionDto model)
        {
            var model = new UserEditionDto()
            {
                Id = 1,
                FirstName = "Max",
                LastName = "Müller",
                Email = "müller123@gmail.com",
                Password = "password12345",
                RoleId = 3,
                Telephone = "+12345567"
            };
            var user = new User()
            {
                Id = 1,
                FirstName = "Max",
                LastName = "Müller",
                Email = "müller123@gmail.com",
                Password = "password12345",
                RoleId = 3,
                Telephone = "+12345567"
            };

            _userRepo.Setup(x => x.GetUserAsync(It.IsAny<int>())).Returns(Task.FromResult(user));
            _userRepo.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);
            var controller = new UsersController(_userRepo.Object, _roleRepo.Object, _customerRepo.Object, _employeeRepo.Object, _mapper.Object);

            //Act
            IActionResult actionResult = await controller.UpdateUser(1, model);
            var OkResult = actionResult as OkObjectResult;
            var value = OkResult.Value as string;
            //Assert
            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
            Assert.Equal("Edition done.", value);
        }

        [Fact]
        public async Task UsersController_DeleteUser()    //(int modelId)
        {
            var user = new User()
            {
                Id = 1,
                FirstName = "Max",
                LastName = "Müller",
                Email = "test@gmail.com",
                Password = "password12345",
                RoleId = 3,
                Telephone = "+12345567"
            };

            _userRepo.Setup(x => x.GetUserAsync(It.IsAny<int>())).Returns(Task.FromResult(user));
            _userRepo.Setup(x => x.DeleteUser(It.IsAny<User>()));
            _userRepo.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);
            var controller = new UsersController(_userRepo.Object, _roleRepo.Object, _customerRepo.Object, _employeeRepo.Object, _mapper.Object);
            //Act
            IActionResult actionResult = await controller.DeleteUser(1);
            var OkResult = actionResult as OkObjectResult;
            var value = OkResult.Value as string;
            //Assert

            Assert.NotNull(OkResult);
            Assert.NotNull(OkResult.Value);
            Assert.Equal("Deletion done.", value);

        }

















    }
}
