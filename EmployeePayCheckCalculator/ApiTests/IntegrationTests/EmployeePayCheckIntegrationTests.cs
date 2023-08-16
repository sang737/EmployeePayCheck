using Api.Dtos.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Api.Common;
using Api.Interfaces;
using Xunit;
using Api.Models;
using Moq;

namespace ApiTests.IntegrationTests
{
    public class EmployeePayCheckIntegrationTests : IntegrationTest
    {
        [Fact]
        public async Task WhenAskedForAnInvalidEmployeePayCheck_ShouldReturnNotFound()
        {
            var response = await HttpClient.GetAsync("/api/v1/Employees/Paycheck/10");
            await response.ShouldReturn(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task WhenAskedForAnBadEmployeePayCheck_ShouldReturnBadRequest()
        {
            var response = await HttpClient.GetAsync("/api/v1/Employees/Paycheck/0");
            await response.ShouldReturn(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task WhenAskedForAnEmployeePayCheck_ShouldReturnCorrectPayCheck()
        {
            var response = await HttpClient.GetAsync("/api/v1/Employees/Paycheck/1");
            var employeePayCheck = new GetEmployeePaycheckDto()
            {
                Id = 1,
                FirstName = "LeBron",
                LastName = "James",
                AnnualSalary = 75420.99m,
                DateOfBirth = new DateTime(1984, 12, 30),
                DependentCount = 0,
                TotalDeductionsPerPayPeriod = 461.54m,
                GrossSalaryPerPayPeriod = 2900.81m,
                NetSalaryPerPayPeriod = 2439.27m
            };
            employeePayCheck.Deductions.Add($"{Constants.EmployeBaseCost}", 461.54m);

            await response.ShouldReturn(HttpStatusCode.OK, employeePayCheck);
        }


        [Fact]
        public async Task WhenAskedForAnEmployeeWithDependentsPayCheck_ShouldReturnCorrectPayCheck()
        {
            var response = await HttpClient.GetAsync("/api/v1/Employees/Paycheck/2");
            var employeePayCheck = new GetEmployeePaycheckDto()
            {
                Id = 2,
                FirstName = "Ja",
                LastName = "Morant",
                AnnualSalary = 92365.22m,
                DateOfBirth = new DateTime(1999, 8, 10),
                DependentCount = 3,
                TotalDeductionsPerPayPeriod = 1363.35m,
                GrossSalaryPerPayPeriod = 3552.51m,
                NetSalaryPerPayPeriod = 2189.16m
            };
            employeePayCheck.Deductions.Add($"{Constants.EmployeBaseCost}", 461.54m);
            employeePayCheck.Deductions.Add($"{Relationship.Spouse.ToString()}:Spouse", 276.92m);
            employeePayCheck.Deductions.Add($"{Relationship.Child.ToString()}:Child1", 276.92m);
            employeePayCheck.Deductions.Add($"{Relationship.Child.ToString()}:Child2", 276.92m);
            employeePayCheck.Deductions.Add($"{Constants.EmployeeSalaryThreshold}", 71.05m);

            await response.ShouldReturn(HttpStatusCode.OK, employeePayCheck);
        }

        [Fact]
        public async Task WhenAskedForAnEmployeePayCheckWithDependentDeductions_ShouldReturnCorrectPayCheck()
        {
            var response = await HttpClient.GetAsync("/api/v1/Employees/Paycheck/3");
            var employeePayCheck = new GetEmployeePaycheckDto()
            {
                Id = 3,
                FirstName = "Michael",
                LastName = "Jordan",
                AnnualSalary = 80000m,
                DateOfBirth = new DateTime(1963, 2, 17),
                DependentCount = 3,
                TotalDeductionsPerPayPeriod = 1476.92m,
                GrossSalaryPerPayPeriod = 3076.92m,
                NetSalaryPerPayPeriod = 1600.00m
            };
            employeePayCheck.Deductions.Add($"{Constants.EmployeBaseCost}", 461.54m);
            employeePayCheck.Deductions.Add($"{Relationship.DomesticPartner.ToString()}:DP", 369.23m);
            employeePayCheck.Deductions.Add($"{Relationship.Child.ToString()}:FN", 369.23m);
            employeePayCheck.Deductions.Add($"{Relationship.Child.ToString()}:Child2", 276.92m);

            await response.ShouldReturn(HttpStatusCode.OK, employeePayCheck);
        }


    }
}
