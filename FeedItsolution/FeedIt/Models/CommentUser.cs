using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedIt.Models
{
	public class CommentUser
	{
        public Comment comment { get; set; }
        public ApplicationUser user { get; set; }
	}
}