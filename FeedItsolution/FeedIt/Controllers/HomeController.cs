using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using FeedIt.Models;
using FeedIt.Service;

namespace FeedIt.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            FeedLists model = new FeedLists();

            string strID = User.Identity.GetUserId();
            model.userFeed = NewsFeedService.Instance.getFeedForUser(strID);

            model.groupsFeed = NewsFeedService.Instance.getFeedForGroups(strID);

            model.allFeed = NewsFeedService.Instance.getAllPosts(strID);

            return View(model);
           // return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult GroupProfileView()
        {
            return View();
        }

    }
}