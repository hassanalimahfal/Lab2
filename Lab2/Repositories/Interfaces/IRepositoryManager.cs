namespace Lab2.Repositories.Interfaces
{
    public interface IRepositoryManager
    {
        IStudentRepository StudentRepository { get; }
        ITeacherRepository TeacherRepository { get; }
        ISubjectRepository SubjectRepository { get; }
        IUnitOfWork UnitOfWork { get; }
    }
}
