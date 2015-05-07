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

        public List<Post> getFeedForUser(int userID)
        {
            var db = new ApplicationDbContext();

            var followings = (from s in db.Followers
                              where s.follower == userID
                              select s).ToList();

            List<Post> postsList = new List<Post>();

            foreach(var s in followings)
            {
                var userPosts = (from b in db.UserPosts
                                 where b.userID == s.following
                                 select b).ToList();
                foreach (var c in userPosts)
                {
                    var post = (from n in db.Posts
                                where n.ID == c.postID
                                select n).SingleOrDefault();

                    postsList.Add(post);
                }
            }
            var dateOrdered = postsList.OrderBy(x => x.date).Take(15).ToList();
            return dateOrdered;
        }

        public List<Post> getFeedForGroups(int userID)
        {
            var db = new ApplicationDbContext();

            var groups = (from s in db.GroupFollowers
                              where s.userID == userID
                              select s).ToList();

            List<Post> postsList = new List<Post>();

            foreach(var d in groups)
            {
                var groupPosts = (from b in db.GroupPosts
                                 where d.groupID == b.groupID
                                 select b).ToList();
                foreach (var c in groupPosts)
                {
                    var post = (from n in db.Posts
                                where n.ID == c.postID
                                select n).SingleOrDefault();

                    postsList.Add(post);
                }
            }
            var dateOrdered = postsList.OrderBy(x => x.date).Take(15).ToList();
            return dateOrdered;
        }

        public List<Post> getAllPosts(int userID)
        {
            List<Post> userPosts = getFeedForUser(userID);
            List<Post> groupPosts = getFeedForGroup(userID);
            foreach(var s in userPosts)
            {
                groupPosts.Add(s);
            }
            var dateOrdered = groupPosts.OrderBy(x => x.date).Take(15).ToList();
            return dateOrdered;
        }

        public List<Post> getAllPostsFromUser(int userID)
        {
            var db = new ApplicationDbContext();

            var userIds = (from b in db.UserPosts
                         where b.userID == userID
                         select b).ToList();

            List<Post> allPosts = new List<Post>();

            foreach(var c in userIds)
            {
                var singlePost = (from k in db.Posts
                                where k.ID == c.postID
                                select k).SingleOrDefault();
                allPosts.Add(singlePost);
            }

             var dateOrdered = allPosts.OrderBy(x => x.date).ToList();
            return dateOrdered;           
        }

        public List<Post> getFeedForGroup(int groupID)
        {
            var db = new ApplicationDbContext();

            var groupPosts = (from b in db.GroupPosts
                              where b.groupID == groupID
                              select b).ToList();

            List<Post> allPosts = new List<Post>();

            foreach (var c in groupPosts)
            {
                var singlePost = (from k in db.Posts
                                  where k.ID == c.postID
                                  select k).SingleOrDefault();
                allPosts.Add(singlePost);
            }

            var dateOrdered = allPosts.OrderBy(x => x.date).ToList();
            return dateOrdered;
        }
    }
}