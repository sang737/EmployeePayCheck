using Api.Dtos.Employee;
using Api.Models;

namespace Api.Interfaces
{
    public interface IDeductions
    {
        public Dictionary<string,decimal> GetDeductions(GetEmployeeDto employee);
    }
}
