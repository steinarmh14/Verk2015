using System;
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
            var db = new ApplicationDbContext();

            db.Groups.Add(group);
            db.SaveChanges();
        }

        public void deleteGroup(int groupID)
        {
            var db = new ApplicationDbContext();

            var group = (from s in db.Groups
                        where s.ID == groupID
                        select s).FirstOrDefault();
            db.Groups.Remove(group);
        }

        public void editGroup(int ID, Group group)
        {
            var db = new ApplicationDbContext();

            var currGroup = (from s in db.Groups
                             where s.ID == ID
                             select s).SingleOrDefault();

            currGroup.name = group.name;
            currGroup.about = group.about;

            db.SaveChanges();
        }

        public Group getGroupByID(int groupID)
        {
            var db = new ApplicationDbContext();

            var group = (from s in db.Groups
                        where s.ID == groupID
                        select s).SingleOrDefault();

            return group;
        }

        public List<Group> getGroupsByName(string name)
        {
            var db = new ApplicationDbContext();

            var groups = (from s in db.Groups
                          where s.name == name
                          select s).ToList();

            return groups; 
        }
    }
}