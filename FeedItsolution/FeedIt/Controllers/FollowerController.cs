using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedIt.Controllers
{
    public class FollowerController : Controller
    {
        // GET: Follower
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult follow()
        {
            return View();
        }
        public ActionResult unfollow()
        {
            return View();
        }
    }
}