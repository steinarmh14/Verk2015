using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedIt.Models
{
    public class SearchViewModel
    {
        public IEnumerable<ApplicationUser> users { get; set; }
        public IEnumerable<Group> groups { get; set; }
    }
}