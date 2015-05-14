using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FeedIt.Models;
using FeedIt.Service;

namespace FeedIt.Controllers
{
    public class FollowerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Follower
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult showFollowers(string userID)
        {
            FollowerService followerService = new FollowerService(db);

            if (!String.IsNullOrEmpty(userID))
            {
                List<ApplicationUser> users = new List<ApplicationUser>();
                users = followerService.getFollowers(userID);
                return View(users);
            }
            else
            {
                return View();
            }
        }

        public ActionResult showFollowing(string userID)
        {
            FollowerService followerService = new FollowerService(db);

            if (!String.IsNullOrEmpty(userID))
            {
                List<ApplicationUser> users = new List<ApplicationUser>();
                users = followerService.getFollowing(userID);
                return View(users);
            }
            else
            {
                return View();
            }
        }
    }
}