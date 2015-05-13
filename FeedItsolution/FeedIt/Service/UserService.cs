using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FeedIt.Models;

namespace FeedIt.Service
{
    public class UserService
    {
        /*private static UserService instance;

        public static UserService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserService();
                }
                return instance;
            }
        }*/

        private readonly ApplicationDbContext _db;

        public UserService(ApplicationDbContext context = null)
        {
            _db = context ?? new ApplicationDbContext();
        }

        public ApplicationUser getUserByID(string id)
        {
                var user = (from s in _db.Users
                            where s.Id == id
                            select s).SingleOrDefault();

                return user;
        }

        public void editUser(string userID, string about, string picture)
        {

                 var edit = (from s in _db.Users
                            where s.Id == userID
                           select s).SingleOrDefault();
                 edit.aboutMe = about;
                 edit.profilePicture = picture;
                 _db.SaveChanges();          
        }
       
        public void deleteUser(int userID)
        {
            
        }
    }
}