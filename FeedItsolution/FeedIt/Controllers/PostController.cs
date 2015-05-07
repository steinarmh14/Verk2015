using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FeedIt.Models;
using FeedIt.Service;
using Microsoft.AspNet.Identity;

namespace FeedIt.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index(int id)
        {
            Post post = PostService.Instance.getPostById(id);
            return View(post);
        }

        [HttpPost]
        public ActionResult createPost(FormCollection collection)
        {
            string about = collection["about"];
            string picture = collection["picture"];

            Post post = new Post();

            // til að byrja með er ratingið alltaf 0!!!!! fix later
            post.about = about;
            post.picture = picture;
            post.date = DateTime.Now;
            post.rateCount = 0;
            post.rating = 0;

            string strID = User.Identity.GetUserId();
            Console.WriteLine(strID);

            PostService.Instance.createPost(post, strID);
            return View();
        }

        [HttpPost]
        public ActionResult ratePost(int? postID, int rating)
        {
            if (postID.HasValue)
            {
                int realPostID = postID.Value;
                PostService.Instance.rate(realPostID, rating);
                return View();
            }
            return View("Error");
        }

        [HttpPost]
        public ActionResult comment(string content,int? postID)
        {
            if (postID.HasValue)
            {
                int realPostID = postID.Value;
                Comment comment = new Comment();
                comment.comment = content;
                comment.date = DateTime.Now;
                comment.postID = realPostID;
                string strID = User.Identity.GetUserId();
                comment.ownerID = strID;

                PostService.Instance.addComment(comment, realPostID);
                return View();
            }
            return View("Error");
        }

        public ActionResult getComments(int? postID)
        {
            if (postID.HasValue)
            {
                int realPostID = postID.Value;
                List<Comment> comments = new List<Comment>();

                comments = PostService.Instance.getCommentsForPost(realPostID);
                return View(comments);
            }
            return View("Error");
        }

        [HttpPost]
        public ActionResult deleteComment(int? commentID)
        {
            if (commentID.HasValue)
            {
                int realCommentID = commentID.Value;
                PostService.Instance.deleteComment(realCommentID);
            }
            return View();
        }

        [HttpPost]
        public ActionResult addDescription(string description, int? postID)
        {
            if (postID.HasValue)
            {
                int realPostID = postID.Value;
                PostService.Instance.addDescription(description, realPostID);
            }
            return View();
        }

    }
}