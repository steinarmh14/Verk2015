﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedIt.Models
{
    public class UserRating
    {
        public int ID { get; set; }
        public string userID { get; set; }
        public int postID { get; set; }
        public int rating { get; set; }
    }
}