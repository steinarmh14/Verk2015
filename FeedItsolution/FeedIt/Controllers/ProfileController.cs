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
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Profile
        public ActionResult Index()
        {
            ProfileService profileService = new ProfileService(db);
            NewsFeedService newsFeedService = new NewsFeedService(db);
            FollowerService followerService = new FollowerService(db);


            string userID = User.Identity.GetUserId();

            if (!String.IsNullOrEmpty(userID))
            {
                List<UserFeed> profileFeed = new List<UserFeed>();
                ApplicationUser user = new ApplicationUser();
                user = profileService.getProfileByID(userID);
                IEnumerable<Post> posts = newsFeedService.getAllPostsFromUser(userID);

                foreach (var post in posts)
                {
                    UserFeed singleUserFeed = new UserFeed();
                    singleUserFeed.user = user;
                    singleUserFeed.post = post;
                    profileFeed.Add(singleUserFeed);
                }
                ProfileViewModel model = new ProfileViewModel();
                model.feed = profileFeed;
                model.user = user;
                model.followers = followerService.getFollowers(userID);
                model.followings = followerService.getFollowing(userID);
                return View(model);
            }
            return View("Error");
        }
        public ActionResult Profile(string userID)
        {
            FollowerService followerService = new FollowerService(db);
            ProfileService profileService = new ProfileService(db);
            NewsFeedService newsFeedService = new NewsFeedService(db);

            if(!String.IsNullOrEmpty(userID))
            {
                string theUser = User.Identity.GetUserId();
                if(userID == theUser)
                {
                    return RedirectToAction("Index");
                }
                if (followerService.isFollower(User.Identity.GetUserId(), userID))
                {
                    List<UserFeed> profileFeed = new List<UserFeed>();
                    ApplicationUser user = new ApplicationUser();
                    user = profileService.getProfileByID(userID);
                    IEnumerable<Post> posts = newsFeedService.getAllPostsFromUser(userID);

                    foreach (var post in posts)
                    {
                        UserFeed singleUserFeed = new UserFeed();
                        singleUserFeed.user = user;
                        singleUserFeed.post = post;
                        profileFeed.Add(singleUserFeed);
                    }
                    ProfileViewModel model = new ProfileViewModel();
                    model.feed = profileFeed;
                    model.user = user;
                    model.followers = followerService.getFollowers(userID);
                    model.followings = followerService.getFollowing(userID);
                    return View(model);
                }
                return RedirectToAction("NotFollowingProfile", new { userID = userID });               
            }
            return View("Error");
        }

        public ActionResult NotFollowingProfile (string userID)
        {
            ProfileService profileService = new ProfileService(db);
            NewsFeedService newsFeedService = new NewsFeedService(db);
            FollowerService followerService = new FollowerService(db);

            List<UserFeed> profileFeed = new List<UserFeed>();
            ApplicationUser user = new ApplicationUser();
            user = profileService.getProfileByID(userID);
            IEnumerable<Post> posts = newsFeedService.getAllPostsFromUser(userID);

            foreach (var post in posts)
            {
                UserFeed singleUserFeed = new UserFeed();
                singleUserFeed.user = user;
                singleUserFeed.post = post;
                profileFeed.Add(singleUserFeed);
            }
            ProfileViewModel model = new ProfileViewModel();
            model.feed = profileFeed;
            model.user = user;
            model.followers = followerService.getFollowers(userID);
            model.followings = followerService.getFollowing(userID);
            return View(model);
        }

        public ActionResult MyProfileView()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditProfile(FormCollection collection)
        {
            ProfileService profileService = new ProfileService(db);

            string userID = User.Identity.GetUserId();
            string aboutMe = collection["aboutMe"];
            string profilePicture = collection["profilePicture"];
            string fullName = collection["fullName"];

            profileService.editUser(userID, aboutMe, fullName, profilePicture);

            return RedirectToAction("Profile", new { userID = userID });
        }

        public ActionResult EditProfile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult deleteProfile(FormCollection collection)
        {
            ProfileService profileService = new ProfileService(db);

            string userID = collection["userID"];
            profileService.deleteUser(userID);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Follow(FormCollection collection)
        {
            FollowerService followerService = new FollowerService(db);

            string userID = collection["userID"];
            if(!String.IsNullOrEmpty(userID))          
            {
                string strID = User.Identity.GetUserId();

                followerService.addFollower(strID, userID);
            }
            //return RedirectToAction("Profile", new { userID =  userID });
            return Json(userID, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Unfollow(FormCollection collection)
        {
            FollowerService followerService = new FollowerService(db);

            string userID = collection["userID"];
            if (!String.IsNullOrEmpty(userID))
            {
                string strID = User.Identity.GetUserId();

                followerService.removeFollower(strID, userID);
            }
            //return RedirectToAction("Profile", new { userID = userID });
            return Json(userID, JsonRequestBehavior.AllowGet);
        }
    }
}