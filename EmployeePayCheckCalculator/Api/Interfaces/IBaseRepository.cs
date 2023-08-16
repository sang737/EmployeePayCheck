namespace Api.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetById(int id);

        Task<IEnumerable<T>> GetAll();
        
    }
}
