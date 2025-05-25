using Lab2.Data;
using Lab2.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab2.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly dbFirstLabContext context;

        public GenericRepository(dbFirstLabContext context)
        {
            this.context = context;
        }
        public T Add(T entity)
        {
            try
            {
               var res= context.Add(entity);
                
                return res.Entity;

            }
            catch (Exception ex)
            {
                //throw ex;
                Console.WriteLine(ex);
                return default(T);
            }
        }

        public bool Delete(T entity)
        {
            try
            {
                var res = context.Remove(entity);
                if (res.State == EntityState.Deleted)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<T> GetAll()
        {
            return context.Set<T>().ToList();   
        }

        public T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }

        

        public T Update(T entity)
        {
            try
            {
               var res= context.Update(entity);
                if(res.State == EntityState.Modified)
                {
                    return res.Entity;  
                }
                return default(T);
            }
            catch(Exception ex)
            {
                return default(T);
            }
        }
    }
}
