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

        public List<UserFeed> getFeedForUser(string userID)
        {
            using (var db = new ApplicationDbContext())
            {
                var followings = (from s in db.Followers
                                  where s.follower == userID
                                  select s).ToList();

                List<UserFeed> postsList = new List<UserFeed>();

                foreach(var s in followings)
                {
                    var posts = (from b in db.Posts
                                 where b.owner == s.following && b.groupID == -1
                                 select b).ToList();
                    foreach (var c in posts)
                    {
                        UserFeed userFeed = new UserFeed();
                           userFeed.post = (from n in db.Posts
                                       where n.ID == c.ID
                                       select n).SingleOrDefault();
                           userFeed.user = (from h in db.Users
                                            where h.Id == userFeed.post.owner
                                            select h).SingleOrDefault();
                           postsList.Add(userFeed);
                    }
                }
                var dateOrdered = postsList.OrderByDescending(x => x.post.date).Take(15).ToList();
                return dateOrdered;
            }
           
        }

        public List<UserFeed> getFeedForGroups(string userID)
        {
            using (var db = new ApplicationDbContext())
            {
                var groups = (from s in db.GroupFollowers
                              where s.userID == userID
                              select s).ToList();

                List<UserFeed> postsList = new List<UserFeed>();

                foreach(var d in groups)
                {
                    UserFeed userFeed = new UserFeed();
                    userFeed.post = (from n in db.Posts
                                     where n.groupID == d.groupID
                                     select n).FirstOrDefault();
                    if(userFeed.post != null)
                    {
                        userFeed.user = (from h in db.Users
                                     where h.Id == userFeed.post.owner
                                     select h).SingleOrDefault();
                        postsList.Add(userFeed);
                    }
                    
                }
                var dateOrdered = postsList.OrderByDescending(x => x.post.date).Take(15).ToList();
                return dateOrdered;
            }
            
        }

        public List<UserFeed> getAllPosts(string userID)
        {
            List<UserFeed> userPosts = getFeedForUser(userID);
            List<UserFeed> groupPosts = getFeedForGroups(userID);
            foreach(var s in userPosts)
            {
                groupPosts.Add(s);
            }
            var dateOrdered = groupPosts.OrderByDescending(x => x.post.date).Take(15).ToList();
            return dateOrdered;
        }

        public List<Post> getAllPostsFromUser(string userID)
        {
            using (var db = new ApplicationDbContext())
            {
                var posts = (from b in db.Posts
                             where b.owner == userID
                             select b).ToList();

                var dateOrdered = posts.OrderByDescending(x => x.date).ToList();
                return dateOrdered;           
            }
        }

        public List<Post> getFeedForGroup(int groupID)
        {
            using (var db = new ApplicationDbContext())
            {
                var posts = (from b in db.Posts
                             where b.groupID == groupID
                             select b).ToList();

                var dateOrdered = posts.OrderByDescending(x => x.date).ToList();
                return dateOrdered;
            }
        }

    }
}