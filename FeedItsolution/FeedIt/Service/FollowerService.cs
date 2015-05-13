using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedIt.Models;

namespace FeedIt.Service
{
    public class FollowerService
    {
        /*private static FollowerService instance;

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
        }*/

        private readonly ApplicationDbContext _db;

        public FollowerService(ApplicationDbContext context = null)
        {
            _db = context ?? new ApplicationDbContext();
        }

        public void addFollower(string followerID, string followingID)
        {

                Follower follower = new Follower { follower = followerID, following = followingID };

                _db.Followers.Add(follower);
                _db.SaveChanges();

        }

        public bool isFollower(string followerID, string followingID)
        {
            

                var follower = (from s in _db.Followers
                                where s.following == followingID && s.follower == followerID
                                select s).FirstOrDefault();
                if(follower == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }

        }

        public void removeFollower(string followerID, string followingID)
        {

                var follower = (from s in _db.Followers
                                where s.following == followingID && s.follower == followerID
                                select s).FirstOrDefault();

                _db.Followers.Remove(follower);
                _db.SaveChanges();
        }

        public List<ApplicationUser> getFollowers(string userID)
        {

                 var followers = (from s in _db.Followers
                                  where s.following == userID
                                  select s).ToList();
                 List<ApplicationUser> result = new List<ApplicationUser>();

                 foreach(var s in followers)
                 {
                       var person = (from b in _db.Users
                                     where b.Id == s.follower
                                     select b).FirstOrDefault();
                        result.Add(person);
                  }
                  return result;
        }

        public List<ApplicationUser> getFollowing(string userID)
        {

                    var followings = (from s in _db.Followers
                                      where s.follower == userID
                                      select s).ToList();
                   List<ApplicationUser> result = new List<ApplicationUser>();

                   foreach (var s in followings)
                   {
                       var person = (from b in _db.Users
                                     where b.Id == s.following
                                     select b).FirstOrDefault();
                       result.Add(person);
                    }
                    return result;
        }
    }
}