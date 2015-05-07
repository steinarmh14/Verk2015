using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedIt.Models
{
    public class GroupFollower
    {
        public int ID { get; set; }
        public int groupID { get; set; }
        public string userID { get; set; }
    }
}