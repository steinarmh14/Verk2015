using FeedIt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedIt.Service
{
    public class ProfileService
    {
        private static ProfileService instance;

        public static ProfileService Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new ProfileService();
                }
                return instance;
            }
        }

        public ApplicationUser getProfileByID(string userID)
        {
            ApplicationUser user;
            using (var db = new ApplicationDbContext())
            {
                user = (from s in db.Users
                            where s.Id == userID
                            select s).SingleOrDefault();
            }
            return user;
        }

        public List<ApplicationUser> getProfilesByName(string name)
        {
            using (var db = new ApplicationDbContext())
            {
                var profileNames = (from s in db.Users
                                    where s.UserName == name
                                    select s).ToList();
                return profileNames;
            }

            
        }

        public void editUser(string userID, string about, string fullName, string profilePicture)
        {
            using (var db = new ApplicationDbContext())
            {
                var edit = (from s in db.Users
                            where s.Id == userID
                            select s).SingleOrDefault();
                if(!String.IsNullOrEmpty(about))
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
                db.SaveChanges();
            }
        }
        public void deleteUser(string userID)
        {
            using(var db = new ApplicationDbContext())
            {
                var remove = (from s in db.Users
                            where s.Id == userID
                            select s).SingleOrDefault();
                db.Users.Remove(remove);
                db.SaveChanges();
            }
        }
    }
}