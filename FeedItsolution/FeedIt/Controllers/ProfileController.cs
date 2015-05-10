﻿using System;
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
        public ActionResult Profile(string userID)
        {
            if(!String.IsNullOrEmpty(userID))
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
                return View(profileFeed);
            }
            return View("Error");
        }

        public ActionResult MyProfileView()
        {
            return View();
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