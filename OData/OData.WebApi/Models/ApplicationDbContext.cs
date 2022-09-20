using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using System.Diagnostics.Metrics;

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
                    new { SchoolName = "Mercury Middle School", MailAddress = "Argentina" },
                    new { SchoolName = "Venus High School", MailAddress = "Armenia" },
                    new { SchoolName = "Earth Univerity", MailAddress = "Australia" },
                    new { SchoolName = "Mars Elementary School ", MailAddress = "Austria" },
                    new { SchoolName = "Jupiter College", MailAddress = "Azerbaijan" },
                    new { SchoolName = "Saturn Middle School", MailAddress = "Bahamas" },
                    new { SchoolName = "Uranus High School", MailAddress = "Bahrain" },
                    new { SchoolName = "Neptune Elementary School", MailAddress = "Bahrain" },
                    new { SchoolName = "Pluto University", MailAddress = "Bahrain" });

            modelBuilder.Entity<School>().OwnsOne(x => x.MailAddress)
                .HasData(
                    new { AptNo = "AR", City = "AR", Street = "AR", ZipCode = "AR" },
                    new { AptNo = "AM", City = "AR", Street = "AR", ZipCode = "AR" },
                    new { AptNo = "AU", City = "AR", Street = "AR", ZipCode = "AR" },
                    new { AptNo = "AT", City = "AR", Street = "AR", ZipCode = "AR" },
                    new { AptNo = "AZ", City = "AR", Street = "AR", ZipCode = "AR" },
                    new { AptNo = "BS", City = "AR", Street = "AR", ZipCode = "AR" },
                    new { AptNo = "BH", City = "AR", Street = "AR", ZipCode = "AR" });
        }
    }
}
