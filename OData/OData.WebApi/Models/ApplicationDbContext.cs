using Microsoft.EntityFrameworkCore;

namespace OData.WebApi.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<School> Schools { get; set; } = default!;

    public DbSet<Student> Students { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<School>().Ignore(s => s.ContactEmails);
        modelBuilder.Entity<School>().Ignore(s => s.BranchAddresses);
        modelBuilder.Entity<School>().HasKey(x => x.SchoolId);

        modelBuilder.Entity<School>()
            .HasData(
                new { SchoolId = 1, SchoolName = "Mercury Middle School", Emails = "[\"info@mercury.com, help@mercury.com\"]", Branches = "[{\"Street\":\"1615 Anzac Avenue\",\"City\":\"Kallangur\",\"ZipCode\":\"4503\",\"AptNo\":27},{\"Street\":\"712 NE Avenue\",\"City\":\"LasVegs\",\"ZipCode\":\"9872\",\"AptNo\":127}]" },
                new { SchoolId = 2, SchoolName = "Venus High School", Emails = "[\"water@venus.com\"]", Branches = "[{\"Street\":\"987 venus Avenue\",\"City\":\"Venus\",\"ZipCode\":\"9871\",\"AptNo\":7}]" },
                new { SchoolId = 3, SchoolName = "Earth Univerity", Emails = "[\"humen@earth.com,animals@earth.com,plants@earth.com\"]", Branches = "[{\"Street\":\"156TH AVE NE\",\"City\":\"Redmond\",\"ZipCode\":\"98004\",\"AptNo\":23},{\"Street\":\"148TH NE Avenue\",\"City\":\"Bellevue\",\"ZipCode\":\"98006\",\"AptNo\":101}]" },
                new { SchoolId = 4, SchoolName = "Mars Elementary School ", Emails = "[\"weather@mars.com\"]", Branches = "[{\"Street\":\"119TH Rd\",\"City\":\"Langur\",\"ZipCode\":\"4503\",\"AptNo\":27}]" },
                new { SchoolId = 5, SchoolName = "Jupiter College", Emails = "[\"news@jupiter.org\"]", Branches = "[{\"Street\":\"87TH Avenue\",\"City\":\"Gur\",\"ZipCode\":\"4503\",\"AptNo\":27},{\"Street\":\"19TH NE Avenue\",\"City\":\"Legs\",\"ZipCode\":\"42\",\"AptNo\":13}]" },
                new { SchoolId = 6, SchoolName = "Saturn Middle School", Emails = "[\"support@saturn.com\"]", Branches = "[{\"Street\":\"242 Avenue\",\"City\":\"Sammm\",\"ZipCode\":\"503\",\"AptNo\":27},{\"Street\":\"228 Avenue\",\"City\":\"Seatle\",\"ZipCode\":\"172\",\"AptNo\":17}]" },
                new { SchoolId = 7, SchoolName = "Uranus High School", Emails = "[\"info@uranus.com\"]", Branches = "[{\"Street\":\"242 SE 37TH\",\"City\":\"Issaqur\",\"ZipCode\":\"029\",\"AptNo\":10}]" },
                new { SchoolId = 8, SchoolName = "Neptune Elementary School", Emails = "[\"tech@neptune.org, support@neptune.edu\"]", Branches = "[{\"Street\":\"Avenue NE\",\"City\":\"Angur\",\"ZipCode\":\"503\",\"AptNo\":2},{\"Street\":\"11 NE SE\",\"City\":\"Lgs\",\"ZipCode\":\"902\",\"AptNo\":111}]" },
                new { SchoolId = 9, SchoolName = "Pluto University", Emails = "[\"priciple@pluto.edu, support@pluto.com\"]", Branches = "[{\"Street\":\"Main ST\",\"City\":\"Kallr\",\"ZipCode\":\"112\",\"AptNo\":27},{\"Street\":\"Sub NE\",\"City\":\"Veglas\",\"ZipCode\":\"134\",\"AptNo\":32}]" }
            );

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

        //modelBuilder.Entity<Student>()
        //    .HasOne<School>()
        //    .WithMany()
        //    .HasForeignKey(p => p.SchoolId);

        // modelBuilder.Entity<School>().OwnsMany(x => x.Students)
        modelBuilder.Entity<Student>()
             .HasData(
                // Mercury school
                new { SchoolId = 1, StudentId = 10, FirstName = "Spens", LastName = "Alex", FavoriteSport = "Soccer", Grade = 87, BirthDay = new DateOnly(2009, 11, 15) },
                new { SchoolId = 1, StudentId = 11, FirstName = "Jasial", LastName = "Eaine", FavoriteSport = "Basketball", Grade = 45, BirthDay = new DateOnly(1989, 8, 3) },
                new { SchoolId = 1, StudentId = 12, FirstName = "Niko", LastName = "Rorigo", FavoriteSport = "Soccer", Grade = 78, BirthDay = new DateOnly(2019, 5, 5) },
                new { SchoolId = 1, StudentId = 13, FirstName = "Roy", LastName = "Rorigo", FavoriteSport = "Tennis", Grade = 67, BirthDay = new DateOnly(1975, 11, 4) },
                new { SchoolId = 1, StudentId = 14, FirstName = "Zaral", LastName = "Clak", FavoriteSport = "Basketball", Grade = 54, BirthDay = new DateOnly(2008, 1, 4) },

                // Venus school
                new { SchoolId = 2, StudentId = 20, FirstName = "Hugh", LastName = "Briana", FavoriteSport = "Basketball", Grade = 78, BirthDay = new DateOnly(1959, 5, 6) },
                new { SchoolId = 2, StudentId = 21, FirstName = "Reece", LastName = "Len", FavoriteSport = "Basketball", Grade = 45, BirthDay = new DateOnly(2004, 2, 5) },
                new { SchoolId = 2, StudentId = 22, FirstName = "Javanny", LastName = "Jay", FavoriteSport = "Soccer", Grade = 87, BirthDay = new DateOnly(2003, 6, 5) },
                new { SchoolId = 2, StudentId = 23, FirstName = "Ketty", LastName = "Oak", FavoriteSport = "Tennis", Grade = 99, BirthDay = new DateOnly(1998, 7, 25) },

                // Earth School
                new { SchoolId = 3, StudentId = 30, FirstName = "Mike", LastName = "Wat", FavoriteSport = "Tennis", Grade = 93, BirthDay = new DateOnly(1999, 5, 15) },
                new { SchoolId = 3, StudentId = 31, FirstName = "Sam", LastName = "Joshi", FavoriteSport = "Soccer", Grade = 78, BirthDay = new DateOnly(2000, 6, 23) },
                new { SchoolId = 3, StudentId = 32, FirstName = "Kerry", LastName = "Travade", FavoriteSport = "Basketball", Grade = 89, BirthDay = new DateOnly(2001, 2, 6) },
                new { SchoolId = 3, StudentId = 33, FirstName = "Pett", LastName = "Jay", FavoriteSport = "Tennis", Grade = 63, BirthDay = new DateOnly(1998, 11, 7) },

                // Mars School
                new { SchoolId = 4, StudentId = 40, FirstName = "Mike", LastName = "Wat", FavoriteSport = "Soccer", Grade = 64, BirthDay = new DateOnly(2011, 11, 15) },
                new { SchoolId = 4, StudentId = 41, FirstName = "Sam", LastName = "Joshi", FavoriteSport = "Basketball", Grade = 98, BirthDay = new DateOnly(2005, 6, 6) },
                new { SchoolId = 4, StudentId = 42, FirstName = "Kerry", LastName = "Travade", FavoriteSport = "Soccer", Grade = 88, BirthDay = new DateOnly(2011, 5, 13) },

                // Jupiter School
                new { SchoolId = 5, StudentId = 50, FirstName = "David", LastName = "Padron", FavoriteSport = "Tennis", Grade = 77, BirthDay = new DateOnly(2015, 12, 3) },
                new { SchoolId = 5, StudentId = 53, FirstName = "Jeh", LastName = "Brook", FavoriteSport = "Basketball", Grade = 69, BirthDay = new DateOnly(2014, 10, 15) },
                new { SchoolId = 5, StudentId = 54, FirstName = "Steve", LastName = "Johnson", FavoriteSport = "Soccer", Grade = 100, BirthDay = new DateOnly(1995, 3, 2) },

                // Saturn School
                new { SchoolId = 6, StudentId = 60, FirstName = "John", LastName = "Haney", FavoriteSport = "Soccer", Grade = 99, BirthDay = new DateOnly(2008, 12, 1) },
                new { SchoolId = 6, StudentId = 61, FirstName = "Morgan", LastName = "Frost", FavoriteSport = "Tennis", Grade = 17, BirthDay = new DateOnly(2009, 11, 4) },
                new { SchoolId = 6, StudentId = 62, FirstName = "Jennifer", LastName = "Viles", FavoriteSport = "Basketball", Grade = 54, BirthDay = new DateOnly(1989, 3, 15) },

                // Uranus School
                new { SchoolId = 7, StudentId = 72, FirstName = "Matt", LastName = "Dally", FavoriteSport = "Basketball", Grade = 77, BirthDay = new DateOnly(2011, 11, 4) },
                new { SchoolId = 7, StudentId = 73, FirstName = "Kevin", LastName = "Vax", FavoriteSport = "Basketball", Grade = 93, BirthDay = new DateOnly(2012, 5, 12) },
                new { SchoolId = 7, StudentId = 76, FirstName = "John", LastName = "Clarey", FavoriteSport = "Soccer", Grade = 95, BirthDay = new DateOnly(2008, 8, 8) },

                // Neptune School
                new { SchoolId = 8, StudentId = 81, FirstName = "Adam", LastName = "Singh", FavoriteSport = "Tennis", Grade = 92, BirthDay = new DateOnly(2006, 6, 23) },
                new { SchoolId = 8, StudentId = 82, FirstName = "Bob", LastName = "Joe", FavoriteSport = "Soccer", Grade = 88, BirthDay = new DateOnly(1978, 11, 15) },
                new { SchoolId = 8, StudentId = 84, FirstName = "Martin", LastName = "Dalton", FavoriteSport = "Tennis", Grade = 77, BirthDay = new DateOnly(2017, 5, 14) },

                // Pluto School
                new { SchoolId = 9, StudentId = 91, FirstName = "Michael", LastName = "Wu", FavoriteSport = "Soccer", Grade = 97, BirthDay = new DateOnly(2022, 9, 22) },
                new { SchoolId = 9, StudentId = 93, FirstName = "Rachel", LastName = "Wottle", FavoriteSport = "Soccer", Grade = 81, BirthDay = new DateOnly(2022, 10, 5) },
                new { SchoolId = 9, StudentId = 97, FirstName = "Aakash", LastName = "Aarav", FavoriteSport = "Soccer", Grade = 98, BirthDay = new DateOnly(2003, 3, 15) }
            );
    }
}
