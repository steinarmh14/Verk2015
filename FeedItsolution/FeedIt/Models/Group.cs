using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedIt.Models
{
    public class Group
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string about { get; set; }
        public string owner { get; set; }
        public string picture { get; set; }
    }
}