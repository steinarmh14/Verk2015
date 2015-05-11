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
        private static PostService instance;

        public static PostService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PostService();
                }
                return instance;
            }
        }

        public void createPost(Post post, string userID)
        {
            using (var db = new ApplicationDbContext())
            {
                post.owner = userID;
                db.Posts.Add(post);
                db.SaveChanges();
            }
        }

        public void createPostForGroup(Post post, string userID, int groupID)
        {
            using (var db = new ApplicationDbContext())
            {
                post.owner = userID;
                post.groupID = groupID;
                db.Posts.Add(post);
                db.SaveChanges();
            }

        }

        public Post getPostById(int postID)
        {
            using (var db = new ApplicationDbContext())
            {
                Post result = (from s in db.Posts
                               where s.ID == postID
                               select s).SingleOrDefault();
                            return result;
            }
    
        }

        public void rate(int postID,int rating)
        {
            using (var db = new ApplicationDbContext())
            {
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
        }

        public void addComment(Comment comment, int postID)
        {
            using (var db = new ApplicationDbContext())
            {
                comment.postID = postID;
                db.Comments.Add(comment);
                db.SaveChanges();
            }
            
        }

        public List<Comment> getCommentsForPost(int ID)
        {
            using (var db = new ApplicationDbContext())
            {
                var comments = (from s in db.Comments
                                where s.postID == ID
                                select s).ToList();

               var dateOrdered = comments.OrderByDescending(x => x.date).Take(15).ToList();

               return dateOrdered;
            }

            
        }


        public void deleteComment(int commentID)
        {
            using (var db = new ApplicationDbContext())
            {
                var comment = (from s in db.Comments
                               where s.ID == commentID
                               select s).FirstOrDefault();

                            db.Comments.Remove(comment);
                            db.SaveChanges();
            }

        }

        public void addDescription(string description, int postID)
        {
            using (var db = new ApplicationDbContext())
            {
                var post = (from s in db.Posts
                            where s.ID == postID
                            select s).FirstOrDefault();

                            post.about = description;

                            db.SaveChanges();
            }

            
        }

    }
}