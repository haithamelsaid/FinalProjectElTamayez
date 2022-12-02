using Microsoft.AspNetCore.Mvc;
using Graduation_Project.Models;
using Graduation_Project.ViewModels;
using Graduation_Project.Areas.Admin.ViewModels;
using Graduation_Project.AdminRepository;
namespace Graduation_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AssignStudentsToGroupController : Controller
    {
        IStudent istudent;
        IGroup igroup;
        IAssignStudentsGroup iassign;
        CenterDBContext db;
        public AssignStudentsToGroupController(IStudent _istudent , IGroup _igroup , CenterDBContext _db, IAssignStudentsGroup _iassign)
        {
            istudent = _istudent;
            igroup = _igroup;
            db = _db;
            iassign = _iassign;
        }
        [HttpGet]
        public IActionResult AssignStudentGroup(int id)
        {
            List<Student> students = istudent.GetAllStudents();
            Group group = igroup.GetById(id);
            AssignStudentsGroupVM assignStudentsGroup = new AssignStudentsGroupVM()
            {
                Students = students,
                Group=group
            };
            return View(assignStudentsGroup);
        }
        [HttpPost]
        public IActionResult AssignStudentGroup(AssignStudentsGroupVM assignStudentsGroup)
        {


            StudentSubjectGroupTeacher model = new();

            if (ModelState.IsValid == true)
            {

                for (int i = 0; i < assignStudentsGroup.MultiStudents.Length; i++)
                {
                    model.GroupId = assignStudentsGroup.GroupId;

                    model.StudentId = assignStudentsGroup.MultiStudents[i];
                    model.TeacherId = 1;
                    model.SubjectId = assignStudentsGroup.GroupId;
                    
                    iassign.Add(model);

                }
                return RedirectToAction("GetAllRegisteredStudents", "Register");
            }
            else
            {
                return View(model);

            }

        }
    }
}
