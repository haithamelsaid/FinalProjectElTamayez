using Graduation_Project.Models;
using Graduation_Project.Repository;
using Graduation_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace El_Tamayez.Controllers
{
    public class ProfileController : Controller
    {
        IStudentRepositry _studentRepositry;
        IHostingEnvironment _environment;
        IRegisterRepositry registerRepositry;
        public ProfileController(IRegisterRepositry _register,IStudentRepositry studentRepositry,IHostingEnvironment hosting)
        {
            _environment = hosting;
            _studentRepositry = studentRepositry;
            registerRepositry = _register;
        }

        public IActionResult Index()
        {
            Student st = _studentRepositry.GetStudentById(1);
            return View(st);
        }
        public IActionResult Edit()
        {
            Student st = _studentRepositry.GetStudentById(1);
             return View(st);
        }
        [HttpPost]
        public IActionResult UpdateStudent(StudentViewmodel newRegister,int sId)
        {

            if (ModelState.IsValid)
            {
                string imagename = "";
                if (newRegister.Picture != null)
                {
                    // to find folder images from wwwroot (Path of imagesfile)
                    string imagesfile = Path.Combine(_environment.WebRootPath, "images");
                    //get image name 
                    imagename =Guid.NewGuid().ToString()+ newRegister.Picture.FileName;
                    string fullpath = Path.Combine(imagesfile, imagename);
                    //delete old Image from imagesfile
                    string oldimagename = _studentRepositry.GetStudentById(sId).Picture;
                    string fulloldpath = Path.Combine(imagesfile, oldimagename);
                    //check if image chang or not 
                    if (fullpath != fulloldpath)
                    {
                        System.IO.File.Delete(fulloldpath);

                        //to save Newimage in viewmodel
                        newRegister.Picture.CopyTo(new FileStream(fullpath, FileMode.Create));
                    } else if (fulloldpath == fullpath)
                    {
                        newRegister.Picture.CopyTo(new FileStream(fullpath, FileMode.Create));

                    }

                }
                else
                {
                   
                    imagename =_studentRepositry.GetStudentById(sId).Picture;

                }
                Student newsts = new Student() {
                Phone = newRegister.Phone,
                FatherPhone = newRegister.FatherPhone,
                FirstName = newRegister.FirstName,
                birthday = newRegister.birthday,
                LastName = newRegister.LastName,
                national_Id = newRegister.national_Id,
                Gender = newRegister.Gender,
                Year = newRegister.Year,
                Picture = imagename
            };
                Register r = new Register()
                {
                    Phone = newRegister.Phone,
                    FatherPhone = newRegister.FatherPhone,
                    FirstName = newRegister.FirstName,
                    birthday = newRegister.birthday,
                    LastName = newRegister.LastName,
                    national_Id = newRegister.national_Id,
                    Gender = newRegister.Gender,
                    Year = newRegister.Year,
                    Picture = imagename
                };
                _studentRepositry.Update(newsts,sId);
                registerRepositry.Update(r, sId);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Edit");

            }

        }
        
    }
}
