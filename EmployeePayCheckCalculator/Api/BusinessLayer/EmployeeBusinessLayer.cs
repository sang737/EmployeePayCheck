using System.Text.RegularExpressions;
using Api.Common;
using Api.Dtos.Employee;
using Api.Interfaces;
using Api.Models;

namespace Api.BusinessLayer
{
    public class EmployeeBusinessLayer : IEmployeeBusinessLayer
    {
        private readonly IDataAccessLayer _dataAccessLayer;
        private readonly IDeductions _deductions;

        public EmployeeBusinessLayer(IDataAccessLayer dataAccessLayer, IDeductions deductions)
        {
            _dataAccessLayer = dataAccessLayer;
            _deductions = deductions;
        }

        public async Task<GetEmployeeDto?> GetEmployeeById(int employeeId)
        {
            return await _dataAccessLayer.GetEmployeeById(employeeId);
        }

        public async Task<List<GetEmployeeDto?>> GetEmployees()
        {
            var result = await _dataAccessLayer.GetEmployees();
            return result.ToList();
        }

        public async Task<GetEmployeePaycheckDto?> GetPayCheckByEmployeeId(int employeeId)
        {
            var employee = await _dataAccessLayer.GetEmployeeById(employeeId);
            return employee == null ? null : GenerateEmployeePaycheckDto(employee);
        }
        
        private GetEmployeePaycheckDto GenerateEmployeePaycheckDto(GetEmployeeDto employee)
        {

            var deductions = _deductions.GetDeductions(employee);
            var perPayCheckGrossSalary = HelperMethods.FormatDecimal((decimal)employee.Salary / Constants.NumberOfPayChecksPerYear);
            var perPayCheckTotalDeductions = deductions.Sum(item => item.Value);

            return new GetEmployeePaycheckDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                AnnualSalary = employee.Salary,
                DateOfBirth = employee.DateOfBirth,
                DependentCount = employee.Dependents.Count(),
                Deductions = deductions,
                TotalDeductionsPerPayPeriod = perPayCheckTotalDeductions,
                GrossSalaryPerPayPeriod = perPayCheckGrossSalary,
                NetSalaryPerPayPeriod = perPayCheckGrossSalary - perPayCheckTotalDeductions
            };
        }



    }

}
