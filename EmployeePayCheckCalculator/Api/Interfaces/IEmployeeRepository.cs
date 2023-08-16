namespace Api.Interfaces
{
    public interface IEmployeeRepository<T>:IBaseRepository<T> where T : class
    {
    }
}
