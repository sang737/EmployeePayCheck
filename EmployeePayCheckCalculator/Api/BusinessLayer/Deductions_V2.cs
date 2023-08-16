using Api.Common;
using Api.Dtos.Employee;
using Api.Interfaces;
using Api.Models;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace Api.BusinessLayer
{
    public class Deductions_V2 : IDeductions
    {

        private readonly ICollection<GetDeductionsDto> _deductions;

        private readonly ICollection<GetEmployeeDeductionDto> _employeeRuntimeDeductions;

        private readonly Dictionary<string, decimal> _resultDeductions;

        //These can be loaded either from database during run time or from environment variables. 
        private const int AgeThresholdForDependentDeduction = 50;


        public Deductions_V2()
        {
            _resultDeductions = new Dictionary<string, decimal>();

            //Mocking the deductions data. In real time scenario we should get this data from the remote data source. 
            _deductions = new List<GetDeductionsDto>()
            {
                new()
                {
                    Id = 1,
                    Name = "Employee base cost",
                    DeductionType = DeductionType.EmployeeBase,
                    Value= 1000.00m
                },
                new()
                {
                    Id = 2,
                    Name = "Dependent deduction less than 50 years",
                    DeductionType = DeductionType.DependentLessThan50,
                    Value= 600
                },
                new()
                {
                    Id = 3,
                    Name = "Additional dependent deduction greater than 50 years",
                    DeductionType = DeductionType.DependentGreaterThan50,
                    Value= 200
                },
                new()
                {
                    Id = 4,
                    Name = "Employee salary between 80000 and 10000",
                    DeductionType = DeductionType.EmployeeSalary80000And100000,
                    Value= 0.02m
                },
                new()
                {
                    Id = 5,
                    Name = "Employee salary greater than 10000",
                    DeductionType = DeductionType.EmployeeSalaryGreaterThan100000,
                    Value= 0.03m
                }
            };

            //Mocking the employee deductions data. In real time scenario we should customize this data on admin portal and get this data from the remote data source. 
            _employeeRuntimeDeductions = new List<GetEmployeeDeductionDto>()
            {
                new()
                {
                    EmployeeId = 2,
                    DeductionType = DeductionType.EmployeeSalary80000And100000
                },
                new()
                {
                    EmployeeId = 4,
                    DeductionType = DeductionType.EmployeeSalaryGreaterThan100000
                }
            };

        }


        public Dictionary<string, decimal> GetDeductions(GetEmployeeDto employee)
        {
            // Add a deduction to the results.
            void AddDeduction(string name, decimal value) => _resultDeductions.Add(name, value);

            // Calculate the per paycheck deduction amount based on monthly base cost.
            decimal CalculatePerPayCheckDeduction(decimal monthlyBaseCost) =>
                HelperMethods.FormatDecimal((monthlyBaseCost * Constants.NumberOfMonthsPerYear) / Constants.NumberOfPayChecksPerYear);

            // Calculate the deduction amount based on the percentage of the salary.
            decimal CalculatePercentSalaryDeduction(decimal salary, decimal percentage) =>
                CalculatePerPayCheckDeduction((salary * percentage) / Constants.NumberOfMonthsPerYear);

            // Get deduction information by deduction type.
            GetDeductionsDto? GetDeduction(DeductionType type) => _deductions.FirstOrDefault(deduction => deduction.DeductionType == type);

            void ProcessBasePayDeduction()
            {
                // Get the deduction cost for employee's base pay.
                var basePayDeductionCost = GetDeduction(DeductionType.EmployeeBase);

                // Check if a base pay deduction cost is found.
                if (basePayDeductionCost != null)
                {
                    // Add the base pay deduction to the employee's deductions list.
                    AddDeduction(basePayDeductionCost.Name, CalculatePerPayCheckDeduction(basePayDeductionCost.Value));
                }
            }

            void ProcessDependentDeductions()
            {
                // Get deduction costs for dependents of different age ranges.
                var dependentDeductionCost = GetDeduction(DeductionType.DependentLessThan50);
                var additionalDependentDeductionCost = GetDeduction(DeductionType.DependentGreaterThan50);

                // If no deduction cost for dependents less than 50 is found, exit the function.
                if (dependentDeductionCost == null)
                {
                    return;
                }

                // Iterate through each dependent of the employee.
                foreach (var dependent in employee.Dependents)
                {
                    // Calculate the age of the dependent.
                    var age = HelperMethods.CalculateAge(dependent.DateOfBirth);

                    // Determine the deduction cost value based on age and deduction types.
                    var deductionCostValue = age < AgeThresholdForDependentDeduction ? dependentDeductionCost.Value : 
                                             (additionalDependentDeductionCost is null ? dependentDeductionCost.Value : dependentDeductionCost.Value + additionalDependentDeductionCost.Value);

                    // Add the dependent deduction for the current dependent.
                    AddDeduction($"{dependent.Relationship}:{dependent.FirstName}", CalculatePerPayCheckDeduction(deductionCostValue));
                }
            }

            void ProcessRuntimeDeductions()
            {
                //This filtering can happen while getting the data from remo data source.
                // Fetch runtime deductions specific to the current employee.
                var employeeRunTimeDeductions = _employeeRuntimeDeductions.Where(deduction => deduction.EmployeeId == employee.Id).ToList();

                // Process each runtime deduction for the employee.
                foreach (var deduction in employeeRunTimeDeductions)
                {
                    // Get the deduction information based on deduction type.
                    var runtimeDeduction = GetDeduction(deduction.DeductionType);

                    // Check if deduction exists and falls under certain deduction types.
                    if (runtimeDeduction != null && (runtimeDeduction.DeductionType == DeductionType.EmployeeSalary80000And100000 || runtimeDeduction.DeductionType == DeductionType.EmployeeSalaryGreaterThan100000))
                    {
                        // Add the percent-based salary deduction.
                        AddDeduction(runtimeDeduction.Name, CalculatePercentSalaryDeduction(employee.Salary, runtimeDeduction.Value));
                    }
                }
            }

            ProcessBasePayDeduction();
            ProcessDependentDeductions();
            ProcessRuntimeDeductions();

            return _resultDeductions;
        }
    }
}
