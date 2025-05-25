using Lab2.Data;
using Lab2.Repositories.Interfaces;

namespace Lab2.Repositories.Implementations
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly dbFirstLabContext context;

        public UnitOfWork(dbFirstLabContext context)
        {
            this.context = context;
        }
        public int SaveChanges()
        {
            try
            {
                return context.SaveChanges();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
