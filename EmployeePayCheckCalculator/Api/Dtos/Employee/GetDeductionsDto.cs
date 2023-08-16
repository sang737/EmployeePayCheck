using Api.Models;

namespace Api.Dtos.Employee
{
    public class GetDeductionsDto
    {
        public int Id { get; set; }
        public string Name
        {
            get; set;
        }

        public decimal Value { get; set; }


        //public DeductionCategory DeductionCategory { get; set; }

        public DeductionType DeductionType { get; set; }

    }
}
