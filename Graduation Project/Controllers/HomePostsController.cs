using Graduation_Project.Models;
using Graduation_Project.Repository;
using Graduation_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Graduation_Project.Controllers
{
    public class HomePostsController : Controller
    {
        IPostRepository postRepo;
        public HomePostsController(IPostRepository _post)
        {
            postRepo = _post;
        }
        public IActionResult Index()
        {
            List<GetPost> posts = postRepo.GetPostForAdmin();
            return View(posts);
        }

        public IActionResult SaveInsert(Post post)
        {
            //will be changed by loged in admin id
            post.AdminId = 2;
            post.GroupId = 2;
            post.LikeCounter = 0;
            post.PostTime = DateTime.Now;
            postRepo.Insert(post);
            return RedirectToAction(nameof(Index));
        }
    }
}