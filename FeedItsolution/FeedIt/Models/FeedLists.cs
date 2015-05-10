using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedIt.Models;

namespace FeedIt.Models
{
    public class FeedLists
    {
        public IEnumerable<UserFeed> userFeed { get; set; }
        public IEnumerable<UserFeed> groupsFeed { get; set; }
        public IEnumerable<UserFeed> allFeed { get; set; }
    }
}