using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Interfaces;
using Api.Models;
using System.Collections.ObjectModel;
using System.Globalization;

namespace Api.BusinessLayer
{
    public class DataAccessLayer : IDataAccessLayer
    {
        private readonly IEmployeeRepository<Employee> _employeeRepository;

        private readonly IDependentRepository<Dependent> _dependentRepository;

        public DataAccessLayer(IEmployeeRepository<Employee> employeeRepository, IDependentRepository<Dependent> dependentRepository)
        {
            _employeeRepository = employeeRepository;
            _dependentRepository = dependentRepository;
        }

        public async Task<GetDependentDto?> GetDependentById(int dependentId)
        {
            var dependent = await _dependentRepository.GetById(dependentId);
            return MapDependentDto(dependent);   
        }

        public async Task<IEnumerable<GetDependentDto?>> GetDependents()
        {
            var dependents = await _dependentRepository.GetAll();
            return dependents.Select(MapDependentDto);
        }

        public async Task<IEnumerable<GetDependentDto?>> GetDependentsByEmployeeId(int employeeId)
        {
            var employee = await _employeeRepository.GetById(employeeId);
            if (employee is null) throw new ArgumentNullException();

            var dependents = await _dependentRepository.GetDependentsByEmployeeId(employeeId);
            return dependents.Select(MapDependentDto);
        }

        public async Task<GetEmployeeDto?> GetEmployeeById(int employeeId)
        {
            var employee = await _employeeRepository.GetById(employeeId);

            if (employee is null) return null;

            var dependents = await _dependentRepository.GetDependentsByEmployeeId(employee.Id);

            var GetDependentDtos = dependents.Select(MapDependentDto);

            return MapEmployeeDto(employee, GetDependentDtos);
        }

        public async Task<IEnumerable<GetEmployeeDto?>> GetEmployees()
        {
            var employees = await _employeeRepository.GetAll();
            var dependents = await _dependentRepository.GetAll();

            var employeeDtos = new Collection<GetEmployeeDto>();

            foreach (var employee in employees)
            {
                var employeeDependentDtos = await GetDependentsByEmployeeId(employee.Id);
                employeeDtos.Add(MapEmployeeDto(employee, employeeDependentDtos));
            }
            return employeeDtos;
        }

        //This mapping can be configured inside AutoMapper
        private static GetDependentDto? MapDependentDto(Dependent? dependent)
        {
            if (dependent is null) return null;

            return  new GetDependentDto
            {
                Id = dependent.Id,
                FirstName = dependent.FirstName,
                LastName = dependent.LastName,
                DateOfBirth = dependent.DateOfBirth,
                Relationship = dependent.Relationship,
                EmployeeId = dependent.EmployeeId
            };
        }

        //This mapping can be configured inside AutoMapper
        private static GetEmployeeDto? MapEmployeeDto(Employee employee, IEnumerable<GetDependentDto> dependents)
        {
            return new GetEmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Salary = employee.Salary,
                DateOfBirth = employee.DateOfBirth,
                Dependents = dependents.ToList()
            };
        }
    }
}
