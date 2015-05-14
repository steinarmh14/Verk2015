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
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(FormCollection collection)
        {
            ProfileService profileService = new ProfileService(db);
            GroupService groupService = new GroupService(db);

            string search = collection["search"];
            SearchViewModel model = new SearchViewModel();
            model.groups = groupService.getGroupsByName(search);
            model.users = profileService.getProfilesByName(search);
            return View(model);
        }

        public ActionResult searchGroupsByName(string name)
        {
            GroupService groupService = new GroupService(db);

            if (!String.IsNullOrEmpty(name))
            {
                List<Group> groups = new List<Group>();
                groups = groupService.getGroupsByName(name);
                return View(groups);
            }
            else
            {
                return View();
            }
        }
    }
}