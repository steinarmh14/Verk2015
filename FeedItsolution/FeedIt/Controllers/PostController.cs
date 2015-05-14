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
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Post
        public ActionResult Index(int id)
        {
            PostService postService = new PostService(db);
            ProfileService profileService = new ProfileService(db);

            Post post = postService.getPostById(id);
            PostViewModel model = new PostViewModel();
            model.post = post;
            IEnumerable<Comment> comments = postService.getCommentsForPost(id);
            model.user = profileService.getProfileByID(post.owner);
            List<CommentUser> commentList = new List<CommentUser>();
            foreach (var item in comments)
            {
                CommentUser commentUser = new CommentUser();
                commentUser.comment = item;
                ApplicationUser user = profileService.getProfileByID(item.ownerID);
                commentUser.user = user;
                commentList.Add(commentUser);
            }
            model.comments = commentList;
            return View(model);
        }

        [HttpPost]
        public ActionResult ratePost(FormCollection collection)
        {
            PostService postService = new PostService(db);

            string postId = collection["postid"];
            string rateInfo = collection["rateinfo"];
            if (!string.IsNullOrEmpty(postId))
            {
                int id = Int32.Parse(postId);
                int rating = Int32.Parse(rateInfo);
                int currentRating = postService.getCurrentRatingFromUser(User.Identity.GetUserId(), id);
                if (currentRating == -1)
                {
                    postService.rate(id, rating, User.Identity.GetUserId());
                }
                else
                {
                    postService.updateRatingFromUser(User.Identity.GetUserId(), id, rating, currentRating);
                }
                Post post = postService.getPostById(id);
                post.rating = System.Math.Round(post.rating, 1);
                return Json(post, JsonRequestBehavior.AllowGet);
            }
            return View("Error");
        }

        [HttpPost]
        public ActionResult Comment(FormCollection collection)
        {
            string postID = collection["postID"];
            string content = collection["comment"];

            if (string.IsNullOrEmpty(content))
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }

            ProfileService service = new ProfileService(db);
            PostService postService = new PostService(db);

            int realPostID = Int32.Parse(postID);
            Comment comment = new Comment();
            comment.comment = content;
            comment.date = DateTime.Now;
            comment.postID = realPostID;
            string strID = User.Identity.GetUserId();
            comment.ownerID = strID;
            postService.addComment(comment, realPostID);
            CommentUser commentUser = new CommentUser();
            commentUser.comment = comment;
            commentUser.user = service.getProfileByID(strID);

            //Til þess að breta í Json þarf að uncommenta næstu línur
            /*PostViewModel model = new PostViewModel();
            List<CommentUser> cu = new List<CommentUser>();
            cu.Add(commentUser);
            model.comments = cu;
            model.post = postService.getPostById(realPostID);
            model.user = null;*/

            return Json(commentUser, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("Index", new { id = realPostID });
        }

        public ActionResult getComments(int? postID)
        {
            PostService postService = new PostService(db);

            if (postID.HasValue)
            {
                int realPostID = postID.Value;
                List<Comment> comments = new List<Comment>();
                comments = postService.getCommentsForPost(realPostID);
                return View(comments);
            }
            return View("Error");
        }

        [HttpPost]
        public ActionResult deleteComment(int? commentID)
        {
            PostService postService = new PostService(db);

            if (commentID.HasValue)
            {
                int realCommentID = commentID.Value;
                postService.deleteComment(realCommentID);
            }
            return View();
        }

        [HttpPost]
        public ActionResult addDescription(string description, int? postID)
        {
            PostService postService = new PostService(db);
            if (postID.HasValue)
            {
                int realPostID = postID.Value;
                postService.addDescription(description, realPostID);
            }
            return View();
        }

        [HttpPost]
        public ActionResult deletePost(FormCollection collection)
        {
            string strID = collection["postID"];
            if(!String.IsNullOrEmpty(strID))
            {
                int postID = Int32.Parse(strID);
                PostService postService = new PostService(db);
                postService.deletePost(postID);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}