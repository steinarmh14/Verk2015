using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedIt.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult profile()
        {
            return View();
        }
        public ActionResult editProfile()
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