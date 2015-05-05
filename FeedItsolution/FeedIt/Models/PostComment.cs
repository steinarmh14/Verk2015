using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedIt.Models
{
    public class PostComment
    {
        public int ID { get; set; }
        public int commentID { get; set; }
        public int postID { get; set; }
    }
}