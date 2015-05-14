using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedIt.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public string comment { get; set; }
        public string ownerID { get; set; }
        public int postID { get; set; }
        public DateTime date { get; set; }
    }
}