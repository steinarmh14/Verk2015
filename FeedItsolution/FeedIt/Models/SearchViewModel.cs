using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedIt.Models
{
    public class SearchViewModel
    {
        public List<ApplicationUser> users { get; set; }
        public List<Group> groups { get; set; }
    }
}