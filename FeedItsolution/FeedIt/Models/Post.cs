﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FeedIt.Models
{
    public class Post
    {
        public int ID { get; set; }
        public string about { get; set; }
        public string picture { get; set; }
        public double rating { get; set; }
        public int rateCount { get; set; }
        public DateTime date { get; set; }
        public string owner { get; set; }
        public int groupID { get; set; }
    }
}