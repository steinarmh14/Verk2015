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
        private ApplicationDbContext db = new ApplicationDbContext();

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
            GroupService groupService = new GroupService(db);
            NewsFeedService newsFeedService = new NewsFeedService(db);

            if (id.HasValue)
            {
                int realID = id.Value;
                if (groupService.isFollower(realID, User.Identity.GetUserId()))
                {
                IEnumerable<UserFeed> groupFeed = newsFeedService.getFeedForGroup(realID);

                GroupViewModel model = new GroupViewModel();
                model.feed = groupFeed;
                model.group = groupService.getGroupByID(realID);
                model.followers = groupService.getFollowers(realID);
                return View(model);
                }
                else
                {
                    return RedirectToAction("NotFollowingGroup", new { id = id });
                }
            }
            return View("Error");
        }

        public ActionResult NotFollowingGroup(int id)
        {
            NewsFeedService newsFeedService = new NewsFeedService(db);
            GroupService groupService = new GroupService(db);

            IEnumerable<UserFeed> groupFeed = newsFeedService.getFeedForGroup(id);

            GroupViewModel model = new GroupViewModel();
            model.feed = groupFeed;
            model.group = groupService.getGroupByID(id);
            model.followers = groupService.getFollowers(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Follow(FormCollection collection)
        {
            GroupService groupService = new GroupService(db);

            string groupID = collection["groupID"];
            int realGroupID = Int32.Parse(groupID);
            if (!String.IsNullOrEmpty(groupID))
            {
                string strID = User.Identity.GetUserId();
                groupService.followGroup(realGroupID, strID);
            }
            return RedirectToAction("GroupView", new { id = realGroupID });
        }

        [HttpPost]
        public ActionResult Unfollow(FormCollection collection)
        {
            GroupService groupService = new GroupService(db);

            string groupID = collection["groupID"];
            if (!String.IsNullOrEmpty(groupID))
            {
                string strID = User.Identity.GetUserId();
                int realGroupID = Int32.Parse(groupID);
                groupService.unfollowGroup(realGroupID, strID);
            }
            return RedirectToAction("GroupView", new { id = groupID });
        }

        public ActionResult MyGroupsView()
        {
            GroupService groupService = new GroupService(db);

            string userId = User.Identity.GetUserId();

            GroupList groups = new GroupList();
            groups.myGroups = groupService.getGroups(userId);

            return View(groups);
        }

        public ActionResult EditGroupView(int? groupID)
        {
            GroupService groupService = new GroupService(db);

            if(groupID.HasValue)
            {
                int realGroupID = groupID.Value;
                Group group = groupService.getGroupByID(realGroupID);
                return View(group);
            }
            return View("Error");
        }

        public ActionResult EditMyGroupsView()
        {
            GroupService groupService = new GroupService(db);

            string userID = User.Identity.GetUserId();

            List<Group> group = new List<Group>();
            group = groupService.getMyGroups(userID);  

            return View(group);
        }


        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            GroupService groupService = new GroupService(db);

            string name = collection["groupName"];
            string about = collection["aboutGroup"];
            string strID = User.Identity.GetUserId();
            string picture = collection["groupPicture"];

            Group group = new Group();
            group.about = about;
            group.name = name;
            group.owner = strID;
            group.picture = picture;

            if(String.IsNullOrEmpty(group.picture))
            {
                group.picture = "http://www.abc.net.au/news/image/954416-3x2-940x627.jpg";
            }

            groupService.createGroup(group);

            return RedirectToAction("GroupView", new { id = group.ID });
        }

        [HttpPost]
        public ActionResult deleteGroup(int? id)
        {
            GroupService groupService = new GroupService(db);

            if (id.HasValue)
            {
                int realID = id.Value;
                string strID = User.Identity.GetUserId();
                Group group = groupService.getGroupByID(realID);

                if (strID == group.owner)
                {
                    groupService.deleteGroup(realID);
                }
            }
            return RedirectToAction("Manage", "Account");
        }

        public ActionResult editGroup()
        {
            return View();
                
        }

        [HttpPost]
        public ActionResult editGroup(FormCollection collection)
        {
            GroupService groupService = new GroupService(db);

            string groupID = collection["groupID"];
            string groupName = collection["groupName"];
            string description = collection["aboutGroup"];
            string groupPicture = collection["groupPicture"];

            Group group = new Group();
            group.name = groupName;
            group.about = description;
            group.picture = groupPicture;

            groupService.editGroup(Int32.Parse(groupID), group);
             
            return RedirectToAction("GroupView", new { id = groupID });
         
        }

        [HttpPost]
        public ActionResult createPost(FormCollection collection)
        {
            PostService postService = new PostService(db);

            string about = collection["description"];
            string picture = collection["picture"];
            string groupID = collection["groupID"];
            int ID = Int32.Parse(groupID);

            if (String.IsNullOrEmpty(about) || String.IsNullOrEmpty(picture))
            {
                return RedirectToAction("GroupView", new { id = ID });
            }

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
            postService.createPost(post, strID);

            return RedirectToAction("GroupView", new { id = ID });
        }
    }
}