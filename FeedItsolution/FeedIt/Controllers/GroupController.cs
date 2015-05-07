using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FeedIt.Models;
using FeedIt.Service;
using Microsoft.AspNet.Identity;

namespace FeedIt.Controllers
{
    public class GroupController : Controller
    {
        // GET: Group
        public ActionResult Index(int? id)
        {
            if(id.HasValue)
            {
                int realID = id.Value;
                Group model = GroupService.Instance.getGroupByID(realID);
                return View(model);
            }
            return View("Error");
        }

        public ActionResult createGroup(FormCollection collection)
        {
            string name = collection["name"];
            string about = collection["about"];
            string strID = User.Identity.GetUserId();
            int owner = Int32.Parse(strID);

            Group group;
            group.about = about;
            group.name = name;
            group.owner = owner;

            GroupService.Instance.createGroup(group);

            return View();
        }

        public ActionResult deleteGroup(int? id)
        {
            if(id.HasValue)
            {
                string strID = User.Identity.GetUserId();
                int user = Int32.Parse(strID);
                int realID = id.Value;
                Group group = GroupService.Instance.getGroupByID(realID);

                if(user == group.owner)
                {
                    GroupService.Instance.deleteGroup(realID);
                }
            }
            return View();
        }

        public ActionResult editGroup(int? id)
        {
            if (id.HasValue)
            {
                string strID = User.Identity.GetUserId();
                int user = Int32.Parse(strID);
                int realID = id.Value;
                Group group = GroupService.Instance.getGroupByID(realID);

                if (user == group.owner)
                {
                    return View("EditGroupView");
                }
            }
            return View();
        }
    }
}