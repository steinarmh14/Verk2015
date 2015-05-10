using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedIt.Models
{
    public class GroupViewModel
    {
        public IEnumerable<UserFeed> feed { get; set; }
        public Group group { get; set; }
        public IEnumerable<ApplicationUser> followers { get; set; }
    }
}