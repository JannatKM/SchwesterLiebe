using Beauty.Entity.Entities;
using Beauty.Repository.Services;
using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Repository
{
    public class CustomerRepositoryTest
    {
        private CustomerRepository _custRepo;


        [Fact]
        public async Task CustomerRepository_CreateCustomerAsync_DeleteCustomer()  
        {
            //Arrange
            var cust = new Customer
            {
                Id=800,
                User = new User { FirstName = "TestMax", LastName = "Müller", Email = "customer6677889@gmail.com",Telephone="+123456", Password = "12345", RoleId = 2 }
            };
            var _db = await Context.GenerateContext();
           _custRepo = new CustomerRepository(_db);
            //Act
            await _custRepo.CreateCustomerAsync(cust);
            await _custRepo.SaveAsync();
            //Assert
            Assert.True(_db.Customers.Count() > 0);
            var customer = await _custRepo.GetCustomerAsync(800);
            Assert.Equal(customer.Id, 800);

            //Act
            _custRepo.DeleteCustomer(cust);
            await _custRepo.SaveAsync();
            //Assert
            var custNull = await  _custRepo.GetCustomerAsync(800);
            Assert.Null(custNull);
        }

        [Fact]
        public async Task CustomerRepository_GetCustomerAsync_GetCustomerByUserIdAsync()  //(int customerId)
        {
            //Arrange
            var _db = await Context.Seed();
            _custRepo = new CustomerRepository(_db);
            //Act
           var cust=await _custRepo.GetCustomerAsync(1);
            //Assert
            Assert.NotNull(cust);
            Assert.IsType<Customer>(cust);
            Assert.Equal(1, cust.Id);
        
        }



        [Fact]
        public async Task CustomerRepository_GetCustomerByUserIdAsync()  //(int customerId)
        {
            //Arrange
            var _db = await Context.GenerateContext();
            _custRepo = new CustomerRepository(_db);

            var cust = new Customer
            {
             User = new User { Id=987, FirstName = "Helga", LastName = "Müller", Email = "helga123@gmail.com", Telephone="+12345", Password = "password123", RoleId = 2 } 
            };
            //Act
            await _custRepo.CreateCustomerAsync(cust);
            await _custRepo.SaveAsync();
            var customer = await _custRepo.GetCustomerByUserIdAsync(987);
          
            //Assert
            Assert.NotNull(customer);
            Assert.IsType<Customer>(customer);
            Assert.Equal(customer.UserId, 987);
        }

        [Fact]
        public async Task CustomerRepository_GetCustomersAsync()
        {
            //Arrange
            var _db = await Context.Seed();
            _custRepo = new CustomerRepository(_db);
            //Act
           var customers= await _custRepo.GetCustomersAsync();
            //Assert
            Assert.NotNull(customers);
            Assert.IsType<List<Customer>>(customers);
        }

        [Fact]
        public async Task GetCustomerByUserIdAsync_ShouldPreventSQLInjection() 
        {
            // Arrange
            var _db = await Context.Seed();
            _custRepo = new CustomerRepository(_db);
            var maliciousInput = "1; DROP TABLE Customers; --";
            //Act & assert
            await Assert.ThrowsAsync<FormatException>(() => _custRepo.GetCustomerByUserIdAsync(int.Parse(maliciousInput)));
        }

       
   


    }

}

