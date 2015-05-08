using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedIt.Models;

namespace FeedIt.Models
{
    public class FeedLists
    {
        public IEnumerable<Post> userFeed { get; set; }
        public IEnumerable<Post> groupsFeed { get; set; }
        public IEnumerable<Post> allFeed { get; set; }
    }
}