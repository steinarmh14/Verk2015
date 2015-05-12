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

        public void rate(int postID,int rating, string userID)
        {
            using (var db = new ApplicationDbContext())
            {
                Post post = (from s in db.Posts
                             where s.ID == postID
                             select s).SingleOrDefault();

               double currentRating = post.rating;
               int rateCount = post.rateCount;

              double allRatings = currentRating * rateCount;
              allRatings = allRatings + rating;
              rateCount++;
              currentRating = allRatings / rateCount;

               post.rateCount = rateCount;
               post.rating = currentRating;

               var userRating = (from s in db.UserRatings
                                 where s.postID == postID && s.userID == userID
                                 select s).FirstOrDefault();
               if (userRating == null)
               {
                   UserRating uRating = new UserRating();
                   uRating.rating = rating;
                   uRating.userID = userID;
                   uRating.postID = postID;
                   db.UserRatings.Add(uRating);
               }
               else
               {
                   userRating.rating = rating;
               }   
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

        public int getCurrentRatingFromUser (string userID, int postID)
        {
            using (var db = new ApplicationDbContext())
            {
                var userRating = (from s in db.UserRatings
                            where s.postID == postID && s.userID == userID
                            select s).SingleOrDefault();
                if (userRating == null)
                {
                    return -1;
                }
                else
                {
                    return userRating.rating;
                }
            }
        }
        public void updateRatingFromUser (string userID, int postID, int rating, int previousRating)
        {
            using (var db = new ApplicationDbContext())
            {
                var userRating = (from s in db.UserRatings
                                  where s.postID == postID && s.userID == userID
                                  select s).FirstOrDefault();
                if (userRating == null)
                {
                    return;
                }
                else
                {
                    userRating.rating = rating;
                    Post post = (from s in db.Posts
                                 where s.ID == postID
                                 select s).SingleOrDefault();

                    double currentRating = post.rating;
                    int rateCount = post.rateCount;

                    double allRatings = currentRating * rateCount;
                    allRatings = allRatings + rating - previousRating;
                    currentRating = allRatings / rateCount;

                    post.rateCount = rateCount;
                    post.rating = currentRating;
                    db.SaveChanges();
                }
            }
        }

    }
}