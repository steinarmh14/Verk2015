using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FeedIt.Models;
using System.Data.SqlClient;

namespace FeedIt.Service
{
    public class PostService
    {
        public void createPost(Post post)
        {
            var db = new ApplicationDbContext();

            db.Posts.Add(post);
            db.SaveChanges();
            // Todo: link with UserPost table
            /*int _postID = db.Posts.Last().ID;
            new UserPost userPost{ user}
            db.UserPosts.Add(userPost);
            db.SaveChanges();*/
        }

        public Post getPostById(int postID)
        {
            var db = new ApplicationDbContext();

            Post result = (from s in db.Posts
                          where s.ID == postID
                          select s).SingleOrDefault();
            return result;
        }

        public void rate(int postID,int rating)
        {
            var db = new ApplicationDbContext();

            Post post = (from s in db.Posts
                         where s.ID == postID
                         select s).SingleOrDefault();

            double currentRating = post.rating;
            int rateCount = post.rateCount;

            double allRatings = currentRating * rateCount;
            rateCount++;
            currentRating = allRatings / rateCount;

            post.rateCount = rateCount;
            post.rating = rating;
            db.SaveChanges();
        }

        public void addComment(Comment comment)
        {
            var db = new ApplicationDbContext();

            db.Comments.Add(comment);

            //Todo: add to postComment table;
        }

        public List<Comment> getCommentsForPost(int ID)
        {
            var db = new ApplicationDbContext();

            var commentIDs = (from s in db.PostComments
                              where s.postID == ID
                              select s).ToList();

            List<Comment> comments = new List<Comment>();
            foreach (var s in commentIDs)
            {
                Comment currComment = (from c in db.Comments
                                      where c.ID == s.commentID
                                      select c).FirstOrDefault();
                comments.Add(currComment);
            }

            return comments;
        }

        public void deleteComment(int commentID)
        {
            var db = new ApplicationDbContext();

            var comment = (from s in db.Comments
                           where s.ID == commentID
                           select s).FirstOrDefault();

            db.Comments.Remove(comment);

            var postComments = (from s in db.PostComments
                                where s.commentID == commentID
                                select s).ToList();

            foreach (var s in postComments)
            {
                db.PostComments.Remove(s);
            }
        }

        public void addDescription(string description, int postID)
        {
            var db = new ApplicationDbContext();

            var post = (from s in db.Posts
                        where s.ID == postID
                        select s).FirstOrDefault();

            post.about = description;

            db.SaveChanges();
        }
    }
}