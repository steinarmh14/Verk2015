﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedIt.Models
{
    public class PostViewModel
    {
        public Post post { get; set; }
        public IEnumerable<CommentUser> comments { get; set; }
        public ApplicationUser user { get; set; }
    }
}