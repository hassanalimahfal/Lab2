namespace Lab2.Repositories.Interfaces
{
    public interface IGenericRepository<T>
    {
        T Add(T entity);
        T Update(T entity);
        bool Delete(T entity);
        T GetById(int id);
        IEnumerable<T> GetAll();

    }
}
