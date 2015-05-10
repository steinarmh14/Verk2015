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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateGroup()
        {
            return View();
        }

        public ActionResult GroupView(int? id)
        {
            if (id.HasValue)
            {
                int realID = id.Value;
                List<UserFeed> groupFeed = new List<UserFeed>();
                IEnumerable<Post> posts = NewsFeedService.Instance.getFeedForGroup(realID);

                foreach (var post in posts)
                {
                    UserFeed singleUserFeed = new UserFeed();
                    singleUserFeed.post = post;
                    singleUserFeed.user = UserService.Instance.getProfileByID(post.owner);
                    groupFeed.Add(singleUserFeed);
                }
                GroupViewModel model = new GroupViewModel();
                model.feed = groupFeed;
                model.group = GroupService.Instance.getGroupByID(realID);
                model.followers = GroupService.Instance.getFollowers(realID);
                return View(model);
            }
            return View("Error");
        }

        public ActionResult MyGroupsView()
        {
            return View();
        }

        public ActionResult EditGroupView()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            string name = collection["groupName"];
            string about = collection["aboutGroup"];
            string strID = User.Identity.GetUserId();
            string picture = collection["groupPicture"];

            Group group = new Group();
            group.about = about;
            group.name = name;
            group.owner = strID;
            group.picture = picture;

            GroupService.Instance.createGroup(group);

            return RedirectToAction("CreateGroup");
        }

        [HttpPost]
        public ActionResult deleteGroup(int? id)
        {
            if (id.HasValue)
            {
                int realID = id.Value;
                string strID = User.Identity.GetUserId();
                Group group = GroupService.Instance.getGroupByID(realID);

                if (strID == group.owner)
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

        [HttpPost]
        public ActionResult createPost(FormCollection collection)
        {
            string about = collection["description"];
            string picture = collection["picture"];
            string groupID = collection["groupID"];
            int ID = Int32.Parse(groupID);

            Post post = new Post();

            // til að byrja með er ratingið alltaf 0!!!!! fix later
            post.about = about;
            post.picture = picture;
            post.date = DateTime.Now;
            post.rateCount = 0;
            post.rating = 0;
            post.groupID = ID;

            string strID = User.Identity.GetUserId();
            post.owner = strID;
            PostService.Instance.createPost(post, strID);

            return RedirectToAction("GroupView", new { id = ID });
        }
    }
}