using Graduation_Project.Models;
using Graduation_Project.Repository;
using Graduation_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Graduation_Project.Controllers
{
    public class SubjectPostsController : Controller
    {
        IPostRepository postRepository;
        public SubjectPostsController(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        public IActionResult Index(int id)
        {
            List<GetPost> p = postRepository.getPostsForSubject(id);
            ViewBag.subjectId = id;
            return View(p);
        }

        public IActionResult PostContent(int id)
        {
            Post p = postRepository.getPostById(id);
            GetPost post = new GetPost();
            post.PostId = p.Id;
            post.Content = p.Content;
            post.likes = p.LikeCounter;
            post.comments = p.Comments;
            post.PostDate = p.PostTime;
            if (p.Student != null)
            {
                post.PostMakerName = p.Student.FirstName + " " + p.Student.LastName;
                post.PostMakerImage = p.Student.Picture;
            }
            else if (p.Teacher != null)
            {
                post.PostMakerName = p.Teacher.FirstName + " " + p.Teacher.LastName;
                post.PostMakerImage = p.Teacher.Picture;
            }
            return View(post);
        }

        public IActionResult SaveInsert(Post post , int groupId)
        {
            if (ModelState.IsValid)
            {
                post.StudentId = 1;
                post.GroupId = groupId;
                post.LikeCounter = 0;
                post.PostTime = DateTime.Now;
                postRepository.Insert(post);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}