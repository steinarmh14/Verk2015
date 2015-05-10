﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FeedIt.Models;

namespace FeedIt.Service
{
    public class GroupService
    {
        private static GroupService instance;

        public static GroupService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GroupService();
                }
                return instance;
            }
        }

        public void createGroup(Group group)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Groups.Add(group);
                db.SaveChanges();
            }    
        }

        public void followGroup(int groupID, string userID)
        {
            using (var db = new ApplicationDbContext())
            {
                GroupFollower groupFollower = new GroupFollower();
                groupFollower.groupID = groupID;
                groupFollower.userID = userID;
                db.GroupFollowers.Add(groupFollower);
                db.SaveChanges();
            } 
        }

        public List<ApplicationUser> getFollowers(int groupID)
        {
            List<ApplicationUser> result = new List<ApplicationUser>();
            using (var db = new ApplicationDbContext())
            {
                var users = (from s in db.GroupFollowers
                                      where s.groupID == groupID
                                      select s).ToList();
                foreach (var item in users)
                {
                    ApplicationUser user = (from c in db.Users
                                            where c.Id == item.userID
                                            select c).SingleOrDefault();
                    result.Add(user);
                }
            }
            return result;
        }

        public List<Group> getGroups(string userID)
        {
            List<Group> result = new List<Group>();
            using (var db = new ApplicationDbContext())
            {
                var users = (from s in db.GroupFollowers
                             where s.userID == userID
                             select s).ToList();
                foreach (var item in users)
                {
                    Group group = (from c in db.Groups
                                            where c.ID == item.groupID
                                            select c).SingleOrDefault();
                    result.Add(group);
                }
            }
            return result;
        }

        public void deleteGroup(int groupID)
        {
            using (var db = new ApplicationDbContext())
            {
                    var group = (from s in db.Groups
                                 where s.ID == groupID
                                 select s).FirstOrDefault();
                    db.Groups.Remove(group);
                    db.SaveChanges();
                
            }

            
        }

        public void editGroup(int ID, Group group)
        {
            using (var db = new ApplicationDbContext())
            {
                var currGroup = (from s in db.Groups
                                             where s.ID == ID
                                             select s).SingleOrDefault();

                            currGroup.name = group.name;
                            currGroup.about = group.about;

                            db.SaveChanges();
            }

            
        }

        public Group getGroupByID(int groupID)
        {
            using (var db = new ApplicationDbContext())
            {
                var group = (from s in db.Groups
                             where s.ID == groupID
                             select s).SingleOrDefault();
                return group;
            }

            
        }

        public List<Group> getGroupsByName(string name)
        {
            using (var db = new ApplicationDbContext())
            {
                var groups = (from s in db.Groups
                              where s.name == name
                              select s).ToList();

                            return groups;
            }

        }
    }
}