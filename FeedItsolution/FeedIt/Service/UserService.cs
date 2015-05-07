using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FeedIt.Models;

namespace FeedIt.Service
{
    public class UserService
    {
        /*
        public void editUser(U userID)
        {
            var db = new ApplicationDbContext();

            var edit = (from s in db.Users
                        where s.Id == userID
                        select s).SingleOrDefault();
        }
        */
        public void deleteUser(int userID)
        {
            
        }
    }
}