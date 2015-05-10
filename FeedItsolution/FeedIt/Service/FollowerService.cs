using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedIt.Models;

namespace FeedIt.Service
{
    public class FollowerService
    {
        private static FollowerService instance;

        public static FollowerService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FollowerService();
                }
                return instance;
            }
        }
        public void addFollower(string followerID, string followingID)
        {
            using (var db = new ApplicationDbContext())
            {
                Follower follower = new Follower { follower = followerID, following = followingID };

                db.Followers.Add(follower);
                db.SaveChanges();
            }
        }

        public void removeFollower(string followerID, string followingID)
        {
            using (var db = new ApplicationDbContext())
            {
                var follower = (from s in db.Followers
                                where s.following == followingID && s.follower == followerID
                                select s).FirstOrDefault();

                db.Followers.Remove(follower);
                db.SaveChanges();
            }
        }

        public List<ApplicationUser> getFollowers(string userID)
        {
            using (var db = new ApplicationDbContext())
            {
                 var followers = (from s in db.Followers
                                  where s.following == userID
                                  select s).ToList();
                 List<ApplicationUser> result = new List<ApplicationUser>();

                 foreach(var s in followers)
                 {
                       var person = (from b in db.Users
                                     where b.Id == s.follower
                                     select b).FirstOrDefault();
                        result.Add(person);
                  }
                  return result;
            } 
        }

        public List<ApplicationUser> getFollowing(string userID)
        {
            using (var db = new ApplicationDbContext())
            {
                    var followings = (from s in db.Followers
                                      where s.follower == userID
                                      select s).ToList();
                   List<ApplicationUser> result = new List<ApplicationUser>();

                   foreach (var s in followings)
                   {
                       var person = (from b in db.Users
                                     where b.Id == s.following
                                     select b).FirstOrDefault();
                       result.Add(person);
                    }
                    return result;
            }
        }
    }
}