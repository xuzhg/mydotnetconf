using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using System.Diagnostics.Metrics;
using System.Net.Mail;

namespace OData.WebApi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<School> Schools { get; set; } = default!;

        public DbSet<Student> Students { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<School>().HasKey(x => x.SchoolId);
            
            modelBuilder.Entity<School>().HasData(
                    new
                    {
                        SchoolId = 1,
                        SchoolName = "Mercury Middle School",
                        Emails = "[\"Argentina\"]"
                    },
                    new
                    {
                        SchoolId = 2, SchoolName = "Venus High School", Emails = "[\"Argentina\"]"
                    },
                    new
                    {
                        SchoolId = 3, SchoolName = "Earth Univerity", Emails = "[\"Argentina\"]"
                    },
                    new
                    {
                        SchoolId = 4, SchoolName = "Mars Elementary School ", Emails = "[\"Argentina\"]"
                    },
                    new
                    {
                        SchoolId = 5, SchoolName = "Jupiter College", Emails = "[\"Argentina\"]"
                    },
                    new
                    {
                        SchoolId = 6, SchoolName = "Saturn Middle School", Emails = "[\"Argentina\"]"
                    },
                    new
                    {
                        SchoolId = 7, SchoolName = "Uranus High School", Emails = "[\"Argentina\"]"
                    },
                    new
                    {
                        SchoolId = 8, SchoolName = "Neptune Elementary School", Emails = "[\"Argentina\"]"
                    },
                    new
                    {
                        SchoolId = 9, SchoolName = "Pluto University", Emails = "[\"Argentina\"]"
                    });

            modelBuilder.Entity<School>().OwnsOne(x => x.MailAddress)
                .HasData(
                    new { SchoolId = 1, AptNo = 1, City = "AR", Street = "AR", ZipCode = "AR" }
                    );


            modelBuilder.Entity<School>().OwnsMany(x => x.Students)
                .HasData(
                    new { SchoolId = 1, StudentId = 10, Name = "AR", Gender = Gender.Male, BirthDay = new DateOnly(2009, 11, 15) },
                    new { SchoolId = 2, StudentId = 20, Name = "AR", Gender = Gender.Male, BirthDay = new DateOnly(2009, 11, 15) }
                    );
        }
    }
}
