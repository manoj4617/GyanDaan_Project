using Microsoft.EntityFrameworkCore;
using static GyanDyan.Models.Domain;

namespace GyanDyan.DataAccess
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public DbSet<StudentProfile> StudentProfiles { get; set; }
        public DbSet<VolunteerProfile> VolunteerProfiles { get; set; }
        public DbSet<StudentRequirement> StudentRequirements { get; set; }
        public DbSet<VolunteerRequirement> VolunteerRequirements { get; set; }
        public DbSet<OneToOne> OneToOneClass { get; set; }
        public DbSet<Group> GroupsClass { get; set; }
        public DbSet<VolunteerInbox> VolunteerInboxes { get; set; }
        public DbSet<StudentInbox> StudentInboxes { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


        }

    }
}
