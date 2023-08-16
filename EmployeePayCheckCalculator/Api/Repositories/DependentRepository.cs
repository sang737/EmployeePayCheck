using Api.Interfaces;
using Api.Models;

namespace Api.Repositories
{
    public class DependentRepository : IDependentRepository<Dependent>
    {
        private readonly ICollection<Dependent> _dependents;

        //Mocking the dependents data. In real time scenario we should get this data from the remote data source. 
        public DependentRepository()
        {
            _dependents = new List<Dependent>()
            {
               new()
                    {
                        Id = 1,
                        FirstName = "Spouse",
                        LastName = "Morant",
                        Relationship = Relationship.Spouse,
                        DateOfBirth = new DateTime(1998, 3, 3),
                        EmployeeId = 2
                    },
                    new()
                    {
                        Id = 2,
                        FirstName = "Child1",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(2020, 6, 23),
                        EmployeeId = 2
                    },
                    new()
                    {
                        Id = 3,
                        FirstName = "Child2",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(2021, 5, 18),
                        EmployeeId = 2
                    },
                    new()
                    {
                        Id = 4,
                        FirstName = "DP",
                        LastName = "Jordan",
                        Relationship = Relationship.DomesticPartner,
                        DateOfBirth = new DateTime(1964, 1, 2),
                        EmployeeId = 3
                    },
                    new()
                    {
                        Id = 5,
                        FirstName = "FN",
                        LastName = "Jordan",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(1973, 8, 10),
                        EmployeeId = 3
                    },
                    new()
                    {
                        Id = 6,
                        FirstName = "Child2",
                        LastName = "Jordan",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(1983, 8, 10),
                        EmployeeId = 3
                    }
            };
        }

        public Task<IEnumerable<Dependent>> GetAll() => Task.FromResult<IEnumerable<Dependent>>(_dependents);
       
        public Task<Dependent> GetById(int id) => Task.FromResult(_dependents.FirstOrDefault(dependent => dependent.Id == id));

        public Task<IEnumerable<Dependent>> GetDependentsByEmployeeId(int employeeId) => Task.FromResult(_dependents.Where(dependent => dependent.EmployeeId == employeeId));
    }
}
