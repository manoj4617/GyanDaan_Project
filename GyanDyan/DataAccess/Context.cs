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
        public DbSet<StudentAccount> StudentAccounts { get; set; }
        public DbSet<VolunteerProfile> VolunteerProfiles { get; set; }
        public DbSet<VolunteerAccount> VolunteerAccounts { get; set; }
        public DbSet<StudentRequirement> StudentRequirements { get; set; }
        public DbSet<VolunteerRequirement> VolunteerRequirements { get; set; }
        public DbSet<OneToOne> OneToOneClass { get; set; }
        public DbSet<Group> GroupsClass { get; set; }
        public DbSet<VolunteerInbox> VolunteerInboxes { get; set; }
        public DbSet<StudentInbox> StudentInboxes { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StudentAccount>()
                .HasOne(i => i.StudentProfile)
                .WithOne(i => i.StudentAccount)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<VolunteerAccount>()
               .HasOne(i => i.VolunteerProfile)
               .WithOne(i => i.VolunteerAccount)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StudentRequirement>()
                .HasOne(id => id.StudentProfile)
                .WithMany(i => i.StudentRequirements)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<VolunteerRequirement>()
                .HasOne(i => i.VolunteerProfile)
                .WithMany(i => i.VolunteerRequirements)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OneToOne>()
                .HasOne(i => i.StudentProfile)
                .WithMany(i => i.OneToOne)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OneToOne>()
                .HasOne(i => i.StudentRequirement)
                .WithOne(i => i.OneToOne);

            modelBuilder.Entity<OneToOne>()
                .HasOne(i => i.VolunteerProfile)
                .WithMany(i => i.OneToOnes)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OneToOne>()
                .HasOne(i => i.VolunteerRequirement)
                .WithOne(i => i.OneToOnes);

            modelBuilder.Entity<Group>()
                .HasOne(i => i.StudentProfile)
                .WithMany(i => i.InGroupStudent)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Group>()
                .HasOne(i => i.StudentRequirement)
                .WithOne(i => i.Group);


            modelBuilder.Entity<Group>()
                .HasOne(i => i.VolunteerRequirement)
                .WithMany(i => i.InGroupVolunteer);

            modelBuilder.Entity<Group>()
                .HasOne(i => i.VolunteerProfile)
                .WithMany(i => i.Groups)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<VolunteerInbox>()
                .HasOne(i => i.VolunteerProfile)
                .WithMany(i => i.VolunteerInboxes)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
