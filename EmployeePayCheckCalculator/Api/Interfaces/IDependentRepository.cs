using Api.Models;

namespace Api.Interfaces
{
    public interface IDependentRepository<T>: IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetDependentsByEmployeeId(int employeeId);
    }
}
