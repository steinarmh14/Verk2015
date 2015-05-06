using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FeedIt.Models;

namespace FeedIt.Service
{
    public class PostService
    {
        public void createPost(Post post)
        {
            var db = new ApplicationDbContext();

            //Todo: gera
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

            //todo: setja currentRating og rateCount í database
        }

        public void addComment(Comment comment)
        {
            var db = new ApplicationDbContext();

            //Todo: add comment to database;
        }

        public List<Comment> getCommentsForPost(int ID)
        {
            var db = new ApplicationDbContext();

            var commentIDs = (from s in db.PostComments
                              where s.postID == ID
                              select s).ToList();

            List<Comment> comments;
            foreach (var s in commentIDs)
            {
                Comment currComment = from c in db.Comments
                                      where c.ID == s.commentID // breyta öllum foreignkeys í navchar???
                                      select c;
                comments.Add(currComment);
            }

            return comments;
        }

        public void deleteComment(int ID)
        {
            var db = new ApplicationDbContext();

            //Todo: eyða kommenti úr gagnagrunni Comments og PostComments
        }

        public void addDescription(string description)
        {
            //Todo: implement
        }



    }
}