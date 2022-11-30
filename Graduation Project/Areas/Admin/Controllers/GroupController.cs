using Microsoft.AspNetCore.Mvc;
using Graduation_Project.AdminRepository;
using Graduation_Project.Models; 
namespace Graduation_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GroupController : Controller
    {
        IGroup igroup;
        public GroupController(IGroup _igroup)
        {
            igroup = _igroup;
        }
        public IActionResult GetAllGroups()
        {
            return View(igroup.GetAll());
        }
        [HttpGet]
        public IActionResult Add() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Group group)
        {
            if (ModelState.IsValid == true) 
            {
                igroup.Add(group);
                return RedirectToAction(nameof(GetAllGroups));
            }
            return View(group);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(igroup.GetById(id));
        }
        [HttpPost]
        public IActionResult Edit(Group group)
        {
            if (ModelState.IsValid == true)
            {
                igroup.Edit(group);
                return RedirectToAction(nameof(GetAllGroups));
            }
            return View(group);
        }

        public IActionResult Delete(int id) 
        {
            igroup.Delete(id);
            return RedirectToAction(nameof(GetAllGroups));
        }
    }
}
