using Graduation_Project.Models;

namespace Graduation_Project.Repository
{
    public interface IStudentRepositry
    {
        List<Student> GetAllStudents();
        List<Student> GetAllStudentsByYear(int year);
        Student GetStudentById(int id);
        void Insert(Student Student);
        void Update(Student Newstudent,int id);
        void Delete(int id);
    }
}
