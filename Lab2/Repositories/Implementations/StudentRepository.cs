using Lab2.Data;
using Lab2.Models;
using Lab2.Repositories.Interfaces;

namespace Lab2.Repositories.Implementations
{
    public class StudentRepository: GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(dbFirstLabContext context) : base(context)
        {

        }
    }
}
