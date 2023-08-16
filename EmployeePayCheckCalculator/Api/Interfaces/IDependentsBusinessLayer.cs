using Api.Dtos.Dependent;

namespace Api.Interfaces
{
    public interface IDependentsBusinessLayer
    {
        public Task<GetDependentDto?> GetDependentById(int dependentId);

        public Task<List<GetDependentDto?>> GetDependents();

        public Task<List<GetDependentDto?>> GetDependentsByEmployeeId(int employeeId);
    }
}
