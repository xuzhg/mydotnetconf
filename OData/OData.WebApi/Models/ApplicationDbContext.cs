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
                        SchoolId = 1, SchoolName = "Mercury Middle School", Emails = "[\"Argentina\"]"
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

            // config the complex value for MailAddress of each School
            modelBuilder.Entity<School>().OwnsOne(x => x.MailAddress)
                .HasData(
                    new { SchoolId = 1, AptNo = 241, City = "Kirk", Street = "156TH AVE", ZipCode = "98051" },
                    new { SchoolId = 2, AptNo = 101, City = "Belly", Street = "24TH ST", ZipCode = "98029" },
                    new { SchoolId = 3, AptNo = 543, City = "AR", Street = "51TH AVE PL", ZipCode = "98043" },
                    new { SchoolId = 4, AptNo = 123, City = "Issaca", Street = "Mars Rd", ZipCode = "98023" },
                    new { SchoolId = 5, AptNo = 443, City = "Redmond", Street = "Sky Freeway", ZipCode = "78123" },
                    new { SchoolId = 6, AptNo = 11, City = "Moon", Street = "187TH ST", ZipCode = "68133" },
                    new { SchoolId = 7, AptNo = 123, City = "Greenland", Street = "Sun Street", ZipCode = "88155" },
                    new { SchoolId = 8, AptNo = 77, City = "BadCity", Street = "Moon way", ZipCode = "89155" },
                    new { SchoolId = 9, AptNo = 12004, City = "Sahamish", Street = "Unviersal ST", ZipCode = "10293" }
                    );


            modelBuilder.Entity<School>().OwnsMany(x => x.Students)
                .HasData(
                    new { SchoolId = 1, StudentId = 10, Name = "AR", Gender = Gender.Male, BirthDay = new DateOnly(2009, 11, 15) },
                    new { SchoolId = 2, StudentId = 20, Name = "AR", Gender = Gender.Male, BirthDay = new DateOnly(2009, 11, 15) }
                    );
        }
    }
}
