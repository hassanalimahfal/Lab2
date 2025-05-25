using Lab2.Repositories.Interfaces;

namespace Lab2.Repositories.Implementations
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly IStudentRepository studentRepository;
        private readonly ITeacherRepository teacherRepository;
        private readonly ISubjectRepository subjectRepository;
        private readonly IUnitOfWork unitOfWork;

        public RepositoryManager(
            IStudentRepository studentRepository,
            ITeacherRepository teacherRepository,
            ISubjectRepository subjectRepository,
            IUnitOfWork unitOfWork
            )
        {
            this.studentRepository = studentRepository;
            this.teacherRepository = teacherRepository;
            this.subjectRepository = subjectRepository;
            this.unitOfWork = unitOfWork;
        }
        public IStudentRepository StudentRepository => studentRepository;

        public ITeacherRepository TeacherRepository => teacherRepository;

        public ISubjectRepository SubjectRepository => subjectRepository;
        public IUnitOfWork UnitOfWork => unitOfWork;    
    }
}
