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
            string userID = User.Identity.GetUserId();

            if (!String.IsNullOrEmpty(userID))
            {
                List<UserFeed> profileFeed = new List<UserFeed>();
                ApplicationUser user = new ApplicationUser();
                user = ProfileService.Instance.getProfileByID(userID);
                IEnumerable<Post> posts = NewsFeedService.Instance.getAllPostsFromUser(userID);

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
                model.followers = FollowerService.Instance.getFollowers(userID);
                model.followings = FollowerService.Instance.getFollowing(userID);
                return View(model);
            }
            return View("Error");
        }
        public ActionResult Profile(string userID)
        {
            if(!String.IsNullOrEmpty(userID))
            {
                string theUser = User.Identity.GetUserId();
                if(userID == theUser)
                {
                    return RedirectToAction("Index");
                }
                if (FollowerService.Instance.isFollower(User.Identity.GetUserId(), userID))
                {
                    List<UserFeed> profileFeed = new List<UserFeed>();
                    ApplicationUser user = new ApplicationUser();
                    user = ProfileService.Instance.getProfileByID(userID);
                    IEnumerable<Post> posts = NewsFeedService.Instance.getAllPostsFromUser(userID);

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
                    model.followers = FollowerService.Instance.getFollowers(userID);
                    model.followings = FollowerService.Instance.getFollowing(userID);
                    return View(model);
                }
                return RedirectToAction("NotFollowingProfile", new { userID = userID });
                
            }
            return View("Error");
        }

        public ActionResult NotFollowingProfile (string userID)
        {
            List<UserFeed> profileFeed = new List<UserFeed>();
            ApplicationUser user = new ApplicationUser();
            user = ProfileService.Instance.getProfileByID(userID);
            IEnumerable<Post> posts = NewsFeedService.Instance.getAllPostsFromUser(userID);

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
            model.followers = FollowerService.Instance.getFollowers(userID);
            model.followings = FollowerService.Instance.getFollowing(userID);
            return View(model);
        }

        public ActionResult MyProfileView()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditProfile(FormCollection collection)
        {
            string userID = User.Identity.GetUserId();
            string aboutMe = collection["aboutMe"];
            string profilePicture = collection["profilePicture"];
            string fullName = collection["fullName"];

            ProfileService.Instance.editUser(userID, aboutMe, fullName, profilePicture);

            return RedirectToAction("Profile", new { userID = userID });
        }

        public ActionResult EditProfile()
        {
            return View();
        }

        /*[HttpPost]
        public ActionResult deleteProfile(FormCollection collection)
        {

        }*/

        [HttpPost]
        public ActionResult Follow(FormCollection collection)
        {
            string userID = collection["userID"];
            if(!String.IsNullOrEmpty(userID))
            {
                string strID = User.Identity.GetUserId();

                FollowerService.Instance.addFollower(strID, userID);
            }
            return RedirectToAction("Profile", new { userID =  userID });
        }

        [HttpPost]
        public ActionResult Unfollow(FormCollection collection)
        {
            string userID = collection["userID"];
            if(!String.IsNullOrEmpty(userID))
            {
                string strID = User.Identity.GetUserId();

                FollowerService.Instance.removeFollower(strID, userID);
            }
            return RedirectToAction("Profile", new { userID = userID });
        }
    }
}