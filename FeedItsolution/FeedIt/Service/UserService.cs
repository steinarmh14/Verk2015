using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FeedIt.Models;

namespace FeedIt.Service
{
    public class UserService
    {
        private static ProfileService instance;

        public static ProfileService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProfileService();
                }
                return instance;
            }
        }

        public ApplicationUser getUserByID(string id)
        {
            using (var db = new ApplicationDbContext())
            {
                var user = (from s in db.Users
                            where s.Id == id
                            select s).SingleOrDefault();

                return user;
            }
        }

        public void editUser(string userID, string about, string picture)
        {
           using (var db = new ApplicationDbContext())
           {
                 var edit = (from s in db.Users
                            where s.Id == userID
                           select s).SingleOrDefault();
                 edit.aboutMe = about;
                 edit.profilePicture = picture;
                 db.SaveChanges();
           }          
        }
       
        public void deleteUser(int userID)
        {
            
        }
    }
}