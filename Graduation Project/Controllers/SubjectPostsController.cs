using Graduation_Project.Models;
using Graduation_Project.Repository;
using Graduation_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Graduation_Project.Controllers
{
    public class SubjectPostsController : Controller
    {
        IPostRepository postRepository;
        ICommentRepository comment;
        IHostingEnvironment hosting;
        CenterDBContext db;
        public SubjectPostsController(IPostRepository postRepository, ICommentRepository comment, IHostingEnvironment hosting, CenterDBContext db)
        {
            this.postRepository = postRepository;
            this.comment = comment;
            this.hosting = hosting;
            this.db = db;
        }

        public IActionResult Index(int id)
        {
            List<GetPost> p = postRepository.getPostsForSubject(id);
            ViewBag.subjectId = id;
            ViewBag.user = User.Identity.Name;
            return View(p);
        }

        public IActionResult PostContent(int id)
        {
            Post p = postRepository.getPostById(id);
            GetPost post = new GetPost();
            post.PostId = p.Id;
            post.Content = p.Content;
            post.likes = p.LikeCounter;
            post.Image = p.Picture;
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

        public IActionResult SaveInsert(Post post, int groupId, IFormFile pic)
        {
            Account AccountId = db.Accounts.SingleOrDefault(a => a.UserName == LoginController.UserName);
            Student s = db.Students.SingleOrDefault(x => x.AccountId == AccountId.Id);
            post.StudentId = s.Id;
            post.GroupId = groupId;
            post.LikeCounter = 0;
            post.PostTime = DateTime.Now;
            if (pic != null)
            {
                //get images folder 
                string images = Path.Combine(hosting.WebRootPath, "images");
                // get the picture name 
                string filename = pic.FileName;
                //combine the file path and image name to gether to get the full path for the photo
                string fullpath = Path.Combine(images, filename);
                pic.CopyTo(new FileStream(fullpath, FileMode.Create));
                post.Picture = filename;
            }
            postRepository.Insert(post);
            return RedirectToAction("Index", new { id = groupId });
        }

        public IActionResult insertComment(Comment c)
        {
            Account AccountId = db.Accounts.SingleOrDefault(a => a.UserName == LoginController.UserName);
            if (User.IsInRole("Student") == true)
            {
                Student s = db.Students.SingleOrDefault(x => x.AccountId == AccountId.Id);
                c.StudentId = s.Id;
                c.Content += $"@@@{s.FirstName} {s.LastName}";
            }
            else if (User.IsInRole("Teacher") == true)
            {
                Teacher s = db.Teachers.SingleOrDefault(x => x.AccountId == AccountId.Id);
                c.TeacherId = s.Id;
                c.Content += $"@@@{s.FirstName} {s.LastName}";
            }
            else if (User.IsInRole("Admin") == true)
            {
                c.AdminId = 2;
            }
            c.CommentTime = DateTime.Now;
            comment.InsertComment(c);
            return RedirectToAction(nameof(PostContent), new { id = c.postid });
        }

        public void IncrementLikeCounter(int id)
        {
            Post p = postRepository.getPostById(id);
            postRepository.incrementLikeCounter(p);
        }
    }
}