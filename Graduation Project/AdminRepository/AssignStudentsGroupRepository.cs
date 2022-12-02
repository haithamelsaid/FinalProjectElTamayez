using Graduation_Project.Models;
namespace Graduation_Project.AdminRepository
{
    public interface IAssignStudentsGroup 
    {
       bool Add(StudentSubjectGroupTeacher model);
    }


    public class AssignStudentsGroupRepository:IAssignStudentsGroup
    {
        CenterDBContext db;
        public AssignStudentsGroupRepository( CenterDBContext _db)
        {
            db = _db;
        }

        public bool Add(StudentSubjectGroupTeacher model) 
        {
            try 
            {
                db.StudentsSubjectsGroupsTeachers.Add(model);
                db.SaveChanges();
                return true;
            }catch(Exception e) 
            {
                return false;
            }
        }
    }
}
