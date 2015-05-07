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
        // GET: Follower
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult showFollowers(int? userID)
        {
            if(userID.HasValue)
            {
                int realUserID = userID.Value;
                List<ApplicationUser> users = new List<ApplicationUser>();
                users = FollowerService.Instance.getFollowers(realUserID);
                return View(users);
            }

            else
            {
                return View();
            }

        }

        public ActionResult showFollowing(int? userID)
        {
            if (userID.HasValue)
            {
                int realUserID = userID.Value;
                List<ApplicationUser> users = new List<ApplicationUser>();
                users = FollowerService.Instance.getFollowing(realUserID);
                return View(users);
            }

            else
            {
                return View();
            }
        }
    }
}