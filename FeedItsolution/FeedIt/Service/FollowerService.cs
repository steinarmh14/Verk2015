using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedIt.Models;

namespace FeedIt.Service
{
    public class FollowerService
    {
        public void addFollower(int followerID, int followingID)
        {

        }
        public void removeFollower(int followerID, int followingID)
        {

        }
        public List<ApplicationUser> getFollowers(int userID)
        {
            var db = new ApplicationDbContext();
            var followers = (from s in db.Followers
                         where s.following == userID
                         select s).ToList();
            List<ApplicationUser> result;

            foreach(var s in followers)
            {
                var person = (from b in db.Users
                             where Int32.Parse(b.Id) == s.follower
                             select b);
                result.Add(person);
            }

        }
        public List<AccountViewModels> getFollowing(int userID)
        {

        }
    }
}