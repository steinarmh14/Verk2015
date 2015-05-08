using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedIt.Models;

namespace FeedIt.Models
{
    public class FeedLists
    {
        public List<Post> userFeed { get; set; }
        public List<Post> groupsFeed { get; set; }
        public List<Post> allFeed { get; set; }
    }
}