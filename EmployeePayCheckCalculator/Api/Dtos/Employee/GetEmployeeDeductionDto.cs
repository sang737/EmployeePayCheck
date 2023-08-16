using Api.Models;

namespace Api.Dtos.Employee
{
    public class GetEmployeeDeductionDto
    {
        public int EmployeeId { get; set; }

        public DeductionType DeductionType { get; set; }
     }
}
