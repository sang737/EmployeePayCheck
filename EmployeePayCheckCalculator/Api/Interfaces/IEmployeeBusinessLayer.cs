using Api.Dtos.Employee;

namespace Api.Interfaces
{
    public interface IEmployeeBusinessLayer
    {
        public Task<GetEmployeeDto?> GetEmployeeById(int employeeId);

        public Task<List<GetEmployeeDto?>> GetEmployees();

        public Task<GetEmployeePaycheckDto?> GetPayCheckByEmployeeId(int employeeId);
    }
}
