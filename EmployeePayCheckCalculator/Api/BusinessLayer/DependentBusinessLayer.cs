using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Interfaces;

namespace Api.BusinessLayer
{
    public class DependentBusinessLayer : IDependentsBusinessLayer
    {
        private readonly IDataAccessLayer _dataAccessLayer;
        public DependentBusinessLayer(IDataAccessLayer dataAccessLayer)
        {
            _dataAccessLayer = dataAccessLayer;
        }

        public async Task<GetDependentDto?> GetDependentById(int dependentId)
        {
            return await _dataAccessLayer.GetDependentById(dependentId);
        }

        public async Task<List<GetDependentDto?>> GetDependents()
        {
            var result = await _dataAccessLayer.GetDependents();
            return result.ToList();
        }

        public async Task<List<GetDependentDto?>> GetDependentsByEmployeeId(int employeeId)
        {
            var result = await _dataAccessLayer.GetDependentsByEmployeeId(employeeId);
            return result.ToList();
        }
    }
}
