using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedIt.Models
{
    public class UserFeed
    {
        public ApplicationUser user { get; set; }
        public Post post { get; set; }
    }
}