﻿using System;
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
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        public ActionResult Index()
        {
            NewsFeedService newsFeedService = new NewsFeedService(db);

            FeedLists model = new FeedLists();

            string strID = User.Identity.GetUserId();
            model.userFeed = newsFeedService.getFeedForUser(strID);

            model.groupsFeed = newsFeedService.getFeedForGroups(strID);

            model.allFeed = newsFeedService.getAllPosts(strID);

            return View(model);
           // return View();
        }

        [HttpPost]
        public ActionResult createPost(FormCollection collection)
        {
            PostService postService = new PostService(db);
            FollowerService followerService = new FollowerService(db);

            string about = collection["description"];
            string picture = collection["picture"];

            if(String.IsNullOrEmpty(about) || String.IsNullOrEmpty(picture))
            {
                return RedirectToAction("Index", "Home");
            }

            Post post = new Post();

            // til að byrja með er ratingið alltaf 0!!!!! fix later
            post.about = about;
            post.groupID = -1;
            post.picture = picture;
            post.date = DateTime.Now;
            post.rateCount = 0;
            post.rating = 0;

            string strID = User.Identity.GetUserId();
            //Console.WriteLine(strID);

            if(!followerService.isFollower(strID, strID))
            {
                    followerService.addFollower(strID, strID);
            }

            postService.createPost(post, strID);
            //RedirectToAction("~Controllers/Home/Index");
            return RedirectToAction("Index", "Home");
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

    }
}