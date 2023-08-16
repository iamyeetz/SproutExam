using AutoMapper;
using IdentityServer4.Endpoints.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Core.Interface;
using Sprout.Exam.Core.Service;
using Sprout.Exam.Infrastructure.Interface;
using Sprout.Exam.Infrastructure.Models;
using Sprout.Exam.Infrastructure.Repository;
using Sprout.Exam.WebApp.Controllers;
using Sprout.Exam.WebApp.Mappers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static IdentityServer4.Models.IdentityResources;

namespace Sprout.Exam.Tests
{
    [TestClass]
    public class EmployeeTest
    {

        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        SproutExamDbContext _dbContext = new SproutExamDbContext();


        public EmployeeTest()
        {
            Setup();
            _employeeRepository = new EmployeeRepository(_dbContext);
            var myProfile = new AutoMapperProfile();
            var config = new MapperConfiguration(x => x.AddProfile(myProfile));
            _mapper = new Mapper(config);
            _employeeService = new EmployeeService(_employeeRepository, _mapper);
        }

        [TestMethod]
        public async Task GetEmployeeById_ReturnsEmployee_Test()
        {


            //arrange
            var EmployeeController = new EmployeeController(_employeeService);

            //act
            var result = await EmployeeController.GetById(1) as OkObjectResult;
            var employee = (EmployeeDto)result.Value;

            //assert

            Assert.IsNotNull(employee);
            Assert.AreEqual(employee.Id, 1);

        }

        [TestMethod]
        public async Task GetEmployeeByIdNoRecordFound_ReturnsError_Test()
        {


            //arrange
            var EmployeeController = new EmployeeController(_employeeService);

            //act
            var result = await EmployeeController.GetById(5).ConfigureAwait(false) as NotFoundResult;

            //assert
            Assert.AreEqual(StatusCodes.Status404NotFound, result.StatusCode);


        }

        [TestMethod]
        public async Task GetEmployees_ReturnsAllEmployee_Test()
        {


            //arrange
            var EmployeeController = new EmployeeController(_employeeService);

            //act
            var result = await EmployeeController.Get() as OkObjectResult;
            var employee = result.Value;

            //assert

            Assert.IsNotNull(employee);


        }

        [TestMethod]
        public async Task DeleteEmployee_ReturnsDeletedId_Test()
        {


            //arrange
            var EmployeeController = new EmployeeController(_employeeService);

            //act
            var result = await EmployeeController.Delete(1) as OkObjectResult;
            var id = result.Value;

            //assert

            Assert.AreEqual(1, id);


        }
  
        #region Preparing database

        private void Setup()
        {
            var dbOption = new DbContextOptionsBuilder<SproutExamDbContext>()
            .UseInMemoryDatabase("Data Source=DESKTOP-70H3JLI\\MSSQLSERVER02;Initial Catalog=SproutExamDb;Integrated Security=True")
            .Options;
            _dbContext = new SproutExamDbContext(dbOption);
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
            SeedDatabase();

        }

        private void SeedDatabase()
        {
            List<Employee> employeeList = new List<Employee>()
            {
               new Employee
                    {
                        FullName = "Christopher Palisoc",
                        Id = 1,
                        Birthdate = DateTime.Parse("09/25/1992"),
                        EmployeeTypeId = 1,
                        Tin = "123123123",
                        IsDeleted = false
                    },
               new Employee
                    {
                        FullName = "Christopher Palisoc II",
                        Id = 2,
                        Birthdate = DateTime.Parse("09/25/1995"),
                        EmployeeTypeId = 2,
                        Tin = "321321321",
                        IsDeleted = false
                    },
               new Employee
                    {
                        FullName = "Christopher Palisoc III",
                        Id = 3,
                        Birthdate = DateTime.Parse("09/25/1995"),
                        EmployeeTypeId = 1,
                        Tin = "12345678",
                        IsDeleted = true
                    }

            };

            _dbContext.Employees.AddRange(employeeList);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}
