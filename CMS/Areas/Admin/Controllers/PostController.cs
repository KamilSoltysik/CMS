using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.Data;
using CMS.Models;

namespace CMS.Areas.Admin.Controllers
{   
    // /admin/post
    [RouteArea("Admin")]
    [RoutePrefix("post")]

    public class PostController : Controller
    {
        private readonly IPostRepository _repository;

        public PostController() : this(new PostRepository()) { }
        public PostController(IPostRepository repository)
        {
            _repository = repository;

        }
        // GET: Admin/Post
        public ActionResult Index()
        {
            var posts = _repository.GetAll();
            return View(posts);
        }

        // /admin/post/create
        [HttpGet]
        [Route("create")]
        public ActionResult Create()
        {
            var model = new Post() {Tags = new List<string>() {"test-1", "test-2"}};
            return View(model);
        }

        // /admin/post/create
        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _repository.Create(model);

            return RedirectToAction("index");
        }

        // /admin/post/edit/post-to-edit
        [HttpGet]
        [Route("edit/{postId}")]
        public ActionResult Edit(string postId) //id = numer posta do edycji
        {
            // TODO: Odebrać model z bazy danych

            var post = _repository.Get(postId);

            if (post == null)
            {
                return HttpNotFound();
            }

            return View(post);
        }

        // /admin/post/edit/post-to-edit
        [HttpPost]
        [Route("edit/{postId}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string postId, Post model)
        {
            var post = _repository.Get(postId);
            if (post == null)
            {
                return HttpNotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // TODO: zaktualizować model w bazie danych
            _repository.Edit(postId, model);

            return RedirectToAction("index");
        }
    }
}