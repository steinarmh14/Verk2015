using FeedIt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedIt.Service
{
    public class ProfileService
    {
        private readonly ApplicationDbContext _db;

        public ProfileService(ApplicationDbContext context = null)
        {
            _db = context ?? new ApplicationDbContext();
        }

        public ApplicationUser getProfileByID(string userID)
        {
            ApplicationUser user;
            user = (from s in _db.Users
                    where s.Id == userID
                    select s).SingleOrDefault();
            return user;
        }

        public List<ApplicationUser> getProfilesByName(string name)
        {
            var profileNames = (from s in _db.Users
                                where s.UserName.StartsWith(name) || s.UserName.EndsWith(name) || s.fullName.StartsWith(name) || s.fullName.EndsWith(name)
                                select s).ToList();
            return profileNames;
        }

        public void editUser(string userID, string about, string fullName, string profilePicture)
        {
            var edit = (from s in _db.Users
                        where s.Id == userID
                        select s).SingleOrDefault();
            if (!String.IsNullOrEmpty(about))
            {
                edit.aboutMe = about;
            }
            if (!String.IsNullOrEmpty(fullName))
            {
                edit.fullName = fullName;
            }
            if (!String.IsNullOrEmpty(profilePicture))
            {
                edit.profilePicture = profilePicture;
            }
            _db.SaveChanges();
        }

        public void deleteUser(string userID)
        {
            var remove = (from s in _db.Users
                          where s.Id == userID
                          select s).SingleOrDefault();
            _db.Users.Remove(remove);
            _db.SaveChanges();
        }
        public bool isFollower(int groupID, string followingID)
        {
            var follower = (from s in _db.GroupFollowers
                            where s.userID == followingID && s.groupID == groupID
                            select s).FirstOrDefault();
            if (follower == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool isFollower(string followerID, string followingID)
        {
            var follower = (from s in _db.Followers
                            where s.following == followingID && s.follower == followerID
                            select s).FirstOrDefault();
            if (follower == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}