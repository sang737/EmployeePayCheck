using Api.Dtos.Dependent;
using Api.Dtos.Employee;

namespace Api.Interfaces
{
    public interface IDataAccessLayer
    {
        public Task<GetEmployeeDto?> GetEmployeeById(int employeeId);

        public Task<IEnumerable<GetEmployeeDto?>> GetEmployees();

        public Task<IEnumerable<GetDependentDto?>> GetDependents();

        public Task<GetDependentDto?> GetDependentById(int dependentId);

        public Task<IEnumerable<GetDependentDto?>> GetDependentsByEmployeeId(int employeeId);
    }
}
