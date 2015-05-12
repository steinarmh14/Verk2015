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
            PostViewModel model = new PostViewModel();
            model.post = post;
            IEnumerable<Comment> comments = PostService.Instance.getCommentsForPost(id);
            model.user = ProfileService.Instance.getProfileByID(post.owner);
            List<CommentUser> commentList = new List<CommentUser>();
            foreach (var item in comments)
            {
                CommentUser commentUser = new CommentUser();
                commentUser.comment = item;
                ApplicationUser user = ProfileService.Instance.getProfileByID(item.ownerID);
                commentUser.user = user;
                commentList.Add(commentUser);
            }
            model.comments = commentList;
            return View(model);
        }

        [HttpPost]
        public ActionResult ratePost(FormCollection collection)
        {
            string postId = collection["postid"];
            string rateInfo = collection["rateinfo"];
            if (!string.IsNullOrEmpty(postId))
            {
                int id = Int32.Parse(postId);
                int rating = Int32.Parse(rateInfo);
                int currentRating = PostService.Instance.getCurrentRatingFromUser(User.Identity.GetUserId(), id);
                if (currentRating == -1)
                {
                    PostService.Instance.rate(id, rating, User.Identity.GetUserId());
                }
                else
                {
                    PostService.Instance.updateRatingFromUser(User.Identity.GetUserId(), id, rating, currentRating);
                }
                Post post = PostService.Instance.getPostById(id);
                post.rating = System.Math.Round(post.rating, 1);
                return Json(post, JsonRequestBehavior.AllowGet);
            }
            return View("Error");
        }

        [HttpPost]
        public ActionResult Comment(FormCollection collection)
        {
            string postID = collection["postID"];
            string content = collection["content"];
            int realPostID = Int32.Parse(postID);
            Comment comment = new Comment();
            comment.comment = content;
            comment.date = DateTime.Now;
            comment.postID = realPostID;
            string strID = User.Identity.GetUserId();
            comment.ownerID = strID;
            PostService.Instance.addComment(comment, realPostID);
            /*CommentUser commentUser = new CommentUser();
            commentUser.comment = comment;
            commentUser.user = ProfileService.Instance.getProfileByID(strID);
            return Json(commentUser, JsonRequestBehavior.AllowGet);*/
            return RedirectToAction("Index", new { id = realPostID });
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