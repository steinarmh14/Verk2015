using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedIt.Controllers
{
    public class GroupController : Controller
    {
        // GET: Group
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult createGroup()
        {
            return View();
        }
        public ActionResult deleteGroup()
        {
            return View();
        }
        public ActionResult editGroup()
        {
            return View();
        }
        public ActionResult getGroup()
        {
            return View();
        }
        public ActionResult searchGroupsByName()
        {
            return View();
        }
    }
}