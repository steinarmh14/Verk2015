using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace FeedIt.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string fullName { get; set; }
        public string aboutMe { get; set; }
        private string _profilePicture { get; set; }
        public string profilePicture {
            get
            {
                return _profilePicture;
            }
            set
            {
                if(string.IsNullOrEmpty(value))
                {
                    _profilePicture = "http://img1.wikia.nocookie.net/__cb20121001202731/theslenderman/images/c/ce/Question-mark-face.jpg";
                }
                else
                {
                    _profilePicture = value;
                }
                
            }
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        // tables:
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Post> Posts { get; set; }
        
        // link tables:
        public DbSet<Follower> Followers { get; set; }
        public DbSet<GroupFollower> GroupFollowers { get; set; }
        public DbSet<UserRating> UserRatings { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
    }
}