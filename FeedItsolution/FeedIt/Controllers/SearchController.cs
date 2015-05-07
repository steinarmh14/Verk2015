using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FeedIt.Models;
using FeedIt.Service;


namespace FeedIt.Controllers
{
    public class SearchController : Controller
    {
        public ActionResult searchGroupsByName(string name)
        {
            if (!String.IsNullOrEmpty(name))
            {
                List<Group> groups = new List<Group>();
                groups = GroupService.Instance.getGroupsByName(name);
                return View(groups);
            }
            else
            {
                return View();
            }

        }
        /*public ActionResult searchUsersByName(string name)
        {
            if (!String.IsNullOrEmpty(name))
            {
                List<ApplicationUser> users = new List<ApplicationUser>();
                users = UserService.Instance.getGroupsUsersByName(name);
                return View(users);
            }
            else
            {
                return View();
            }
        }*/
    }
}