using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RedemptionNews.Data;
using RedemptionNews.Data.FileManager;
using RedemptionNews.Models;
using RedemptionNews.Models.Comments;
using RedemptionNews.Repository;
using RedemptionNews.ViewModels;

namespace RedemptionNews.Controllers
{
    public class HomeController : Controller
    {
        private IRepository _repo;
        private IFileManager _fileManager;

        public HomeController(IRepository repo, IFileManager fileManager)
        {
            _repo = repo;
            _fileManager = fileManager;
        }
        public IActionResult Index(int pageNumber)
        {
            if (pageNumber < 1)
                return RedirectToAction("index", new { pageNumber = 1 });
            return View(_repo.GetAllPosts(pageNumber));
        }

        //public IActionResult Index(int pageNumber)
        //{
        //    if (pageNumber < 1)
        //        return RedirectToAction("index", new { pageNumber = 1 });

        //    var vm = _repo.GetAllPosts(pageNumber);
        //    return View(vm);
        //}

        public IActionResult Post(int id) =>
           View(_repo.GetPost(id));

        //public IActionResult Post(int id)
        //{
        //    var post = _repo.GetPost(id);
        //    return View(post);
        //}

        [HttpGet("/image/{image}")]
        public IActionResult Image(string image) =>
            new FileStreamResult
            (_fileManager.ImageStream(image),
                $"image /{ image.Substring(image.LastIndexOf(".") + 1)}"
                );
     

        //[HttpGet("/image/{image}")]
        //public IActionResult Image(string image)
        //{
        //    var mime = image.Substring(image.LastIndexOf(".") + 1);
        //    return new FileStreamResult(_fileManager.ImageStream(image), $"image /{ mime}");
        //}

        [HttpPost]
        public async Task<IActionResult> Comment(CommentViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Post", new { id = vm.PostId });
            }
            var post = _repo.GetPost(vm.PostId);
            if (vm.MainCommentId == 0)
            {
                post.MainComments = post.MainComments ?? new List<MainComment>();

                post.MainComments.Add(new MainComment
                {
                    Message = vm.Message,
                    Name = vm.Name,
                    Email = vm.Email,
                    Created = DateTime.Now,
                });

                _repo.UpdatePost(post);
            }
            else
            {
                var comment = new SubComment
                {
                    MainCommentId = vm.MainCommentId,
                    Name = vm.Name,
                    Email = vm.Email,
                    Message = vm.Message,
                    Created = DateTime.Now
                };
                _repo.AddSubComment(comment);

            }
            await _repo.SaveChangesAsync();
            return RedirectToAction("Post", new { id = vm.PostId });
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
