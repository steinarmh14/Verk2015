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

        [HttpPost]
        public ActionResult createGroup(FormCollection collection)
        {
            string name = collection["name"];
            string about = collection["about"];
            string strID = User.Identity.GetUserId();

            Group group = new Group();
            group.about = about;
            group.name = name;
            group.owner = strID;

            GroupService.Instance.createGroup(group);

            return View();
        }

        [HttpPost]
        public ActionResult deleteGroup(int? id)
        {
            if(id.HasValue)
            {
                int realID = id.Value;
                string strID = User.Identity.GetUserId();
                Group group = GroupService.Instance.getGroupByID(realID);

                if(strID == group.owner)
                {
                    GroupService.Instance.deleteGroup(realID);
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult editGroup(int? id)
        {
            if (id.HasValue)
            {
                string strID = User.Identity.GetUserId();
                int realID = id.Value;
                Group group = GroupService.Instance.getGroupByID(realID);

                if (strID == group.owner)
                {
                    return View("EditGroupView");
                }
            }
            return View();
        }
    }
}