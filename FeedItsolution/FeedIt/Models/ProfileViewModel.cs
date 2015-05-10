using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedIt.Models
{
    public class ProfileViewModel
    {
        public IEnumerable<UserFeed> feed { get; set; }
        public ApplicationUser user { get; set; }
        public IEnumerable<ApplicationUser> followers { get; set; }
        public IEnumerable<ApplicationUser> followings { get; set; }
    }
}