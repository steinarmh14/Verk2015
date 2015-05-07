using FeedIt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedIt.Service
{
    public class ProfileService
    {
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

            var profilenames = (from s in db.Users
                                where s.UserName == name
                                select s).ToList();
            
            return profilenames;
        }

        public void editProfile(int id)
        {

        }
    }
}