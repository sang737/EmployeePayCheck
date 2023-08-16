namespace Api.Dtos.Employee
{
    public class GetEmployeePaycheckDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public int DependentCount { get; set; }


        private decimal _annualSalary;
        public decimal AnnualSalary
        {
            get { return _annualSalary; }
            set { _annualSalary = FormatDecimal(value); }
        }


        private decimal _totalDeductionsPerPayPeriod;
        public decimal TotalDeductionsPerPayPeriod
        {
            get { return _totalDeductionsPerPayPeriod; }
            set { _totalDeductionsPerPayPeriod = FormatDecimal(value); }
        }


        private decimal _grossSalaryPerPayPeriod;
        public decimal GrossSalaryPerPayPeriod
        {
            get { return _grossSalaryPerPayPeriod; }
            set { _grossSalaryPerPayPeriod = FormatDecimal(value); }
        }
     

        private decimal _netSalaryPerPayPeriod;
        public decimal NetSalaryPerPayPeriod
        {
            get { return _netSalaryPerPayPeriod; }
            set { _netSalaryPerPayPeriod = FormatDecimal(value); }
        }

        public Dictionary<string, decimal> Deductions { get; set; }

        public Dictionary<string, decimal> Pay { get; set; }

        public GetEmployeePaycheckDto()
        {
            Deductions = new Dictionary<string, decimal>();
            Pay = new Dictionary<string, decimal>();
        }

        private decimal FormatDecimal(decimal value)
        {
            return Math.Round(value, 2);
        }
    }

  
}
