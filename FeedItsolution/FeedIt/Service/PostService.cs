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
        private readonly ApplicationDbContext _db;

        public PostService(ApplicationDbContext context = null)
        {
            _db = context ?? new ApplicationDbContext();
        }

        public void createPost(Post post, string userID)
        {
            post.owner = userID;
            _db.Posts.Add(post);
            _db.SaveChanges();
        }

        public void createPostForGroup(Post post, string userID, int groupID)
        {
            post.owner = userID;
            post.groupID = groupID;
            _db.Posts.Add(post);
            _db.SaveChanges();
        }

        public Post getPostById(int postID)
        {
            Post result = (from s in _db.Posts
                           where s.ID == postID
                           select s).SingleOrDefault();
            return result;
        }

        public void rate(int postID, int rating, string userID)
        {
            Post post = (from s in _db.Posts
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
            var userRating = (from s in _db.UserRatings
                              where s.postID == postID && s.userID == userID
                              select s).FirstOrDefault();
            if (userRating == null)
            {
                UserRating uRating = new UserRating();
                uRating.rating = rating;
                uRating.userID = userID;
                uRating.postID = postID;
                _db.UserRatings.Add(uRating);
            }
            else
            {
                userRating.rating = rating;
            }
            _db.SaveChanges();
        }

        public void addComment(Comment comment, int postID)
        {
            comment.postID = postID;
            _db.Comments.Add(comment);
            _db.SaveChanges();
        }

        public List<Comment> getCommentsForPost(int ID)
        {
            var comments = (from s in _db.Comments
                            where s.postID == ID
                            select s).ToList();
            var dateOrdered = comments.OrderByDescending(x => x.date).Take(15).ToList();
            return dateOrdered;
        }


        public void deleteComment(int commentID)
        {
            var comment = (from s in _db.Comments
                           where s.ID == commentID
                           select s).FirstOrDefault();
            _db.Comments.Remove(comment);
            _db.SaveChanges();
        }

        public void addDescription(string description, int postID)
        {
            var post = (from s in _db.Posts
                        where s.ID == postID
                        select s).FirstOrDefault();
            post.about = description;
            _db.SaveChanges();
        }

        public int getCurrentRatingFromUser(string userID, int postID)
        {
            var userRating = (from s in _db.UserRatings
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

        public void updateRatingFromUser(string userID, int postID, int rating, int previousRating)
        {
            var userRating = (from s in _db.UserRatings
                              where s.postID == postID && s.userID == userID
                              select s).FirstOrDefault();
            if (userRating == null)
            {
                return;
            }
            else
            {
                userRating.rating = rating;
                Post post = (from s in _db.Posts
                             where s.ID == postID
                             select s).SingleOrDefault();
                double currentRating = post.rating;
                int rateCount = post.rateCount;
                double allRatings = currentRating * rateCount;
                allRatings = allRatings + rating - previousRating;
                currentRating = allRatings / rateCount;
                post.rateCount = rateCount;
                post.rating = currentRating;
                _db.SaveChanges();
            }
        }
    }
}