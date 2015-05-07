using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using FeedIt.Service;
using FeedIt.Models;

namespace FeedIt.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            string strID = User.Identity.GetUserId();
            int id = Int32.Parse(strID);

            ApplicationUser user = new ApplicationUser();
            user = ProfileService.Instance.getProfileByID(id);

            return View(user);
        }
        public ActionResult profile(int? userID)
        {
            if(userID.HasValue)
            {
                int realUserID = userID.Value;
                ApplicationUser user = new ApplicationUser();
                user = ProfileService.Instance.getProfileByID(realUserID);

                return View(user);
            }
            return View();
        }

        [HttpPost]
        public ActionResult editProfile(ApplicationUser user)
        {

            return View();
        }

        [HttpPost]
        public ActionResult follow(int? userID)
        {
            if(userID.HasValue)
            {
                int realUserID = userID.Value;

                string strID = User.Identity.GetUserId();
                int id = Int32.Parse(strID);

                FollowerService.Instance.addFollower(id, realUserID);
            }
            return View();
        }

        [HttpPost]
        public ActionResult unfollow(int? userID)
        {
            if (userID.HasValue)
            {
                int realUserID = userID.Value;

                string strID = User.Identity.GetUserId();
                int id = Int32.Parse(strID);

                FollowerService.Instance.removeFollower(id, realUserID);
            }
            return View();
        }
    }
}