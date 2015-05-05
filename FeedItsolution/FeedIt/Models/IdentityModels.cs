using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace FeedIt.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        // tables:
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Follower> Followers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Post> Posts { get; set; }
        
        // link tables:
        public DbSet<GroupPost> GroupPosts { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<UserPost> UserPosts { get; set; }
        public DbSet<GroupFollower> GroupFollowers { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
    }
}