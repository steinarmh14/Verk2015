using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FeedIt.Models;

namespace FeedIt.Service
{
    public class NewsFeedService
    {
        private static NewsFeedService instance;

        public static NewsFeedService Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new NewsFeedService();
                }
                return instance;
            }
        }

        public List<Post> getFeedForUser(string userID)
        {
            var db = new ApplicationDbContext();

            var followings = (from s in db.Followers
                              where s.follower == userID
                              select s).ToList();

            List<Post> postsList = new List<Post>();

            foreach(var s in followings)
            {
                var posts = (from b in db.Posts
                                 where b.owner == s.following
                                 select b).ToList();
                foreach (var c in posts)
                {
                    var post = (from n in db.Posts
                                where n.ID == c.ID
                                select n).SingleOrDefault();

                    postsList.Add(post);
                }
            }
            var dateOrdered = postsList.OrderBy(x => x.date).Take(15).ToList();
            return dateOrdered;
        }

        public List<Post> getFeedForGroups(string userID)
        {
            var db = new ApplicationDbContext();

            var groups = (from s in db.GroupFollowers
                              where s.userID == userID
                              select s).ToList();

            List<Post> postsList = new List<Post>();

            foreach(var d in groups)
            {
                var post = (from n in db.Posts
                            where n.ID == d.groupID
                            select n).ToList();
                foreach (var item in post)
                {
                    postsList.Add(item);
                }
            }
            var dateOrdered = postsList.OrderBy(x => x.date).Take(15).ToList();
            return dateOrdered;
        }

        public List<Post> getAllPosts(string userID)
        {
            List<Post> userPosts = getFeedForUser(userID);
            List<Post> groupPosts = getFeedForGroups(userID);
            foreach(var s in userPosts)
            {
                groupPosts.Add(s);
            }
            var dateOrdered = groupPosts.OrderBy(x => x.date).Take(15).ToList();
            return dateOrdered;
        }

        public List<Post> getAllPostsFromUser(string userID)
        {
            var db = new ApplicationDbContext();

            var posts = (from b in db.Posts
                         where b.owner == userID
                         select b).ToList();

             var dateOrdered = posts.OrderBy(x => x.date).ToList();
            return dateOrdered;           
        }

        public List<Post> getFeedForGroup(int groupID)
        {
            var db = new ApplicationDbContext();

            var posts = (from b in db.Posts
                              where b.groupID == groupID
                              select b).ToList();

            var dateOrdered = posts.OrderBy(x => x.date).ToList();
            return dateOrdered;
        }
    }
}