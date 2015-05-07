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

        public ApplicationUser getProfileByID(int ID)
        {
            var db = new ApplicationDbContext();


            var user = (from s in db.Users
                        where Int32.Parse(s.Id) == ID
                        select s).SingleOrDefault();

            return user;
        }

        public List<ApplicationUser> getProfilesByName(string name)
        {
            var db = new ApplicationDbContext();

            var profileNames = (from s in db.Users
                                where s.UserName == name
                                select s).ToList();
            
            return profileNames;
        }

        public void editProfile(int id)
        {

        }
    }
}