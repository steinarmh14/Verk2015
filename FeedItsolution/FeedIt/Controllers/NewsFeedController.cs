using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FeedIt.Models;
using FeedIt.Service;
using Microsoft.AspNet.Identity;

namespace FeedIt.Controllers
{
    public class NewsFeedController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: NewsFeed
        public ActionResult Index()
        {
            NewsFeedService newsFeedService = new NewsFeedService(db);

            string strID = User.Identity.GetUserId();
            List<UserFeed> model = newsFeedService.getFeedForUser(strID);

            return View(model);
        }

        public ActionResult groupFeed()
        {
            NewsFeedService newsFeedService = new NewsFeedService(db);

            string strID = User.Identity.GetUserId();
            List<UserFeed> model = newsFeedService.getFeedForGroups(strID);

            return View(model);
        }

        public ActionResult allFeed()
        {
            NewsFeedService newsFeedService = new NewsFeedService(db);

            string strID = User.Identity.GetUserId();
            List<UserFeed> model = newsFeedService.getAllPosts(strID);

            return View(model);
        }

        public ActionResult singleGroupFeed(int? groupID)
        {
            NewsFeedService newsFeedService = new NewsFeedService(db);

            if(groupID.HasValue)
            {
                int realGroupID = groupID.Value;
                List<UserFeed> model = newsFeedService.getFeedForGroup(realGroupID);
                return View(model);
            }
            return View("Error");
        }
    }
}