using Api.Common;
using Api.Dtos.Employee;
using Api.Interfaces;
using Api.Models;

namespace Api.BusinessLayer
{
    public class Deductions : IDeductions
    {
        private readonly Dictionary<string, decimal> _deductions;

        //These can be loaded either from database during run time or from environment variables. 
        private const decimal BasePayDeductionCost = 1000;

        private const decimal SalaryThresholdForAdditionalDeduction = 80000;
        private const decimal SalaryAdditionalDeductionPercentage = 0.02m;

        private const decimal DependentDeductionCost = 600;
        private const int AgeThresholdForDependentDeduction = 50;
        private const decimal AdditionalDependentDeductionCost = 200;

        public Deductions()
        {
            _deductions = new Dictionary<string, decimal>();
        }
        public Dictionary<string, decimal> GetDeductions(GetEmployeeDto employee)
        {
            // Calculate the per paycheck deduction amount based on monthly base cost.
            decimal PerPayCheckDeductionCost(decimal monthlyBaseCost) => HelperMethods.FormatDecimal(
                (decimal)monthlyBaseCost * Constants.NumberOfMonthsPerYear / Constants.NumberOfPayChecksPerYear);

            // Calculate the deduction amount based on the percentage of the salary.
            decimal TwoPercentSalaryDeduction(decimal SalaryAdditionalDeductionPercentage) =>
                PerPayCheckDeductionCost((employee.Salary * SalaryAdditionalDeductionPercentage) / Constants.NumberOfMonthsPerYear);

            //employees have a base cost of $1,000 per month(for benefits)
            _deductions.Add($"{Constants.EmployeBaseCost}", PerPayCheckDeductionCost(BasePayDeductionCost));

            //For each dependent
            foreach (var dependent in employee.Dependents)
            {
                //each dependent represents an additional $600 cost per month(for benefits)
                _deductions.Add($"{dependent.Relationship.ToString()}:{dependent.FirstName}",
                    HelperMethods.CalculateAge(dependent.DateOfBirth) < AgeThresholdForDependentDeduction
                        ? PerPayCheckDeductionCost(DependentDeductionCost)
                        //dependents that are over 50 years old will incur an additional $200 per month
                        : PerPayCheckDeductionCost(DependentDeductionCost + AdditionalDependentDeductionCost));
            }

            //For salary greater than 80,000
            if (employee.Salary > SalaryThresholdForAdditionalDeduction)
            {
                _deductions.Add($"{Constants.EmployeeSalaryThreshold}", TwoPercentSalaryDeduction(SalaryAdditionalDeductionPercentage));
            }

            return _deductions;
        }
    }
}
