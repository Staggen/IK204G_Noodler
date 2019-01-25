using DataLayer.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;

namespace DataLayer.Repositories {
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false) {
            Database.Log = s => Debug.WriteLine(s);
        }

        public static ApplicationDbContext Create() {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) { // What is this? - Figure out
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }

        // Start of DbSet(s)

        public DbSet<PostModels> Posts { get; set; }
        public DbSet<FriendModels> Friends { get; set; }
        public DbSet<VisitorModels> Visitors { get; set; }
        public DbSet<RequestModels> Requests { get; set; }
        public DbSet<ProfileModels> Profiles { get; set; }
        public DbSet<FriendCategoryModels> Categories { get; set; }

        // End of DbSet(s)
    }
}
