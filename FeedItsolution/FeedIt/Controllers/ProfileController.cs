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

            ApplicationUser user = new ApplicationUser();
            user = ProfileService.Instance.getProfileByID(strID);

            return View(user);
        }
        public ActionResult profile(string userID)
        {
            if(!String.IsNullOrEmpty(userID))
            {
                ApplicationUser user = new ApplicationUser();
                user = ProfileService.Instance.getProfileByID(userID);

                return View(user);
            }
            return View("Error");
        }

        [HttpPost]
        public ActionResult editProfile(ApplicationUser user)
        {

            return View("Error");
        }

        [HttpPost]
        public ActionResult follow(string userID)
        {
            if(!String.IsNullOrEmpty(userID))
            {
                string strID = User.Identity.GetUserId();

                FollowerService.Instance.addFollower(strID, userID);
            }
            return View();
        }

        [HttpPost]
        public ActionResult unfollow(string userID)
        {
            if(!String.IsNullOrEmpty(userID))
            {
                string strID = User.Identity.GetUserId();

                FollowerService.Instance.removeFollower(strID, userID);
            }
            return View();
        }
    }
}