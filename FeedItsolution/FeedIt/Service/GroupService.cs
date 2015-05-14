using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FeedIt.Models;

namespace FeedIt.Service
{
    public class GroupService
    {
        private readonly ApplicationDbContext _db;

        public GroupService(ApplicationDbContext context = null)
        {
            _db = context ?? new ApplicationDbContext();
        }

        public void createGroup(Group group)
        {
            _db.Groups.Add(group);
            _db.SaveChanges();
            followGroup(group.ID, group.owner);
        }

        public void followGroup(int groupID, string userID)
        {
            GroupFollower groupFollower = new GroupFollower();
            groupFollower.groupID = groupID;
            groupFollower.userID = userID;
            _db.GroupFollowers.Add(groupFollower);
            _db.SaveChanges();
        }

        public void unfollowGroup(int groupID, string userID)
        {
            var follower = (from s in _db.GroupFollowers
                            where s.groupID == groupID && s.userID == userID
                            select s).FirstOrDefault();
            _db.GroupFollowers.Remove(follower);
            _db.SaveChanges();
        }

        public bool isFollower(int groupID, string followingID)
        {
            var follower = (from s in _db.GroupFollowers
                            where s.userID == followingID && s.groupID == groupID
                            select s).FirstOrDefault();
            if (follower == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public List<ApplicationUser> getFollowers(int groupID)
        {
            List<ApplicationUser> result = new List<ApplicationUser>();
            var users = (from s in _db.GroupFollowers
                         where s.groupID == groupID
                         select s).ToList();
            foreach (var item in users)
            {
                ApplicationUser user = (from c in _db.Users
                                        where c.Id == item.userID
                                        select c).SingleOrDefault();
                result.Add(user);
            }
            return result;
        }

        public List<Group> getGroups(string userID)
        {
            List<Group> result = new List<Group>();
            var users = (from s in _db.GroupFollowers
                         where s.userID == userID
                         select s).ToList();
            foreach (var item in users)
            {
                Group group = (from c in _db.Groups
                               where c.ID == item.groupID
                               select c).SingleOrDefault();
                result.Add(group);
            }
            return result;
        }

        public List<Group> getMyGroups(string userID)
        {
            var group = (from s in _db.Groups
                         where s.owner == userID
                         select s).ToList();
            return group;
        }

        public void deleteGroup(int groupID)
        {
            var group = (from s in _db.Groups
                         where s.ID == groupID
                         select s).FirstOrDefault();
            _db.Groups.Remove(group);
            _db.SaveChanges();
        }

        public void editGroup(int ID, Group group)
        {

            var currGroup = (from s in _db.Groups
                             where s.ID == ID
                             select s).SingleOrDefault();
            if (!String.IsNullOrEmpty(group.name))
            {
                currGroup.name = group.name;
            }
            if (!String.IsNullOrEmpty(group.about))
            {
                currGroup.about = group.about;
            }
            if (!String.IsNullOrEmpty(group.picture))
            {
                currGroup.picture = group.picture;
            }
            _db.SaveChanges();
        }

        public Group getGroupByID(int groupID)
        {
            var group = (from s in _db.Groups
                         where s.ID == groupID
                         select s).SingleOrDefault();
            return group;
        }

        public List<Group> getGroupsByName(string name)
        {
            var groups = (from s in _db.Groups
                          where s.name.StartsWith(name) || s.name.EndsWith(name)
                          select s).ToList();

            return groups;
        }
    }
}