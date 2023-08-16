using Api.Dtos.Dependent;
using Api.Interfaces;
using Api.Models;

namespace Api.Repositories;

public class EmployeeRepository : IEmployeeRepository<Employee>
{
    private readonly ICollection<Employee> _employees;

    //Mocking the Employee data. In real time scenario we should get this data from the remote data source. 
    public EmployeeRepository()
    {
        _employees = new List<Employee>()
        {
            new()
            {
                Id = 1,
                FirstName = "LeBron",
                LastName = "James",
                Salary = 75420.99m,
                DateOfBirth = new DateTime(1984, 12, 30)
            },
            new()
            {
                Id = 2,
                FirstName = "Ja",
                LastName = "Morant",
                Salary = 92365.22m,
                DateOfBirth = new DateTime(1999, 8, 10)
            },
            new()
            {
                Id = 3,
                FirstName = "Michael",
                LastName = "Jordan",
                Salary = 80000m,
                DateOfBirth = new DateTime(1963, 2, 17)

            },
            new()
            {
                Id = 4,
                FirstName = "Employee4",
                LastName = "Number4",
                Salary = 150000m,
                DateOfBirth = new DateTime(1963, 2, 17)

            }
        };
    }
    public Task<IEnumerable<Employee>> GetAll() => Task.FromResult<IEnumerable<Employee>>(_employees);

    public Task<Employee> GetById(int id) => Task.FromResult(_employees.FirstOrDefault(employee => employee.Id == id));
}