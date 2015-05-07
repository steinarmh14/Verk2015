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
        // GET: NewsFeed
        public ActionResult Index()
        {
            string strID = User.Identity.GetUserId();
            int id = Int32.Parse(strID);
            List<Post> model = NewsFeedService.Instance.getFeedForUser(id);

            return View(model);
        }

        public ActionResult groupFeed()
        {
            string strID = User.Identity.GetUserId();
            int id = Int32.Parse(strID);
            List<Post> model = NewsFeedService.Instance.getFeedForGroups(id);

            return View(model);
        }

        public ActionResult allFeed()
        {
            string strID = User.Identity.GetUserId();
            int id = Int32.Parse(strID);
            List<Post> model = NewsFeedService.Instance.getAllPosts(id);

            return View(model);
        }

        public ActionResult singleGroupFeed(int? groupID)
        {
            if(groupID.HasValue)
            {
                int realGroupID = groupID.Value;
                List<Post> model = NewsFeedService.Instance.getFeedForGroup(realGroupID);
                return View(model);
            }
            return View("Error");
        }
    }
}