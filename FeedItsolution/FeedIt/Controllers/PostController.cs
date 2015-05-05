using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedIt.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult createPost()
        {
            return View();
        }
        public ActionResult ratePost()
        {
            return View();
        }
        public ActionResult comment()
        {
            return View();
        }
        public ActionResult getComments()
        {
            return View();
        }
        public ActionResult deleteComment()
        {
            return View();
        }
        public ActionResult addDescription()
        {
            return View();
        }
    }
}