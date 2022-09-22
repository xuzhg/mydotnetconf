﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;

namespace OData.WebApi.Models;

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
        modelBuilder.Entity<School>().Ignore(s => s.ContactEmails);
        modelBuilder.Entity<School>().HasKey(x => x.SchoolId);

        modelBuilder.Entity<School>()
            .HasData(
                new { SchoolId = 1, SchoolName = "Mercury Middle School", Emails = "[\"info@mercury.com, help@mercury.com\"]" },
                new { SchoolId = 2, SchoolName = "Venus High School", Emails = "[\"water@venus.com\"]" },
                new { SchoolId = 3, SchoolName = "Earth Univerity", Emails = "[\"humen@earth.com,animals@earth.com,plants@earth.com\"]" },
                new { SchoolId = 4, SchoolName = "Mars Elementary School ", Emails = "[\"weather@mars.com\"]" },
                new { SchoolId = 5, SchoolName = "Jupiter College", Emails = "[\"news@jupiter.org\"]" },
                new { SchoolId = 6, SchoolName = "Saturn Middle School", Emails = "[\"support@saturn.com\"]" },
                new { SchoolId = 7, SchoolName = "Uranus High School", Emails = "[\"info@uranus.com\"]" },
                new { SchoolId = 8, SchoolName = "Neptune Elementary School", Emails = "[\"tech@neptune.org, support@neptune.edu\"]" },
                new { SchoolId = 9, SchoolName = "Pluto University", Emails = "[\"priciple@pluto.edu, support@pluto.com\"]" }
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
                new { SchoolId = 1, StudentId = 10, FirstName = "Spens", LastName = "Alex", Gender = Gender.Male, BirthDay = new DateOnly(2009, 11, 15) },
                new { SchoolId = 1, StudentId = 11, FirstName = "Jasial", LastName = "Eaine", Gender = Gender.Female, BirthDay = new DateOnly(2029, 8, 3) },
                new { SchoolId = 1, StudentId = 12, FirstName = "Niko", LastName = "Rorigo", Gender = Gender.Male, BirthDay = new DateOnly(2019, 5, 5) },
                new { SchoolId = 1, StudentId = 13, FirstName = "Roy", LastName = "Rorigo", Gender = Gender.Male, BirthDay = new DateOnly(2109, 11, 4) },
                new { SchoolId = 1, StudentId = 14, FirstName = "Zaral", LastName = "Clak", Gender = Gender.Female, BirthDay = new DateOnly(2008, 1, 4) },

                // Venus school
                new { SchoolId = 2, StudentId = 20, FirstName = "Hugh", LastName = "Briana", Gender = Gender.Female, BirthDay = new DateOnly(2029, 5, 6) },
                new { SchoolId = 2, StudentId = 21, FirstName = "Reece", LastName = "Len", Gender = Gender.Female, BirthDay = new DateOnly(2034, 2, 5) },
                new { SchoolId = 2, StudentId = 22, FirstName = "Javanny", LastName = "Jay", Gender = Gender.Male, BirthDay = new DateOnly(2023, 6, 5) },
                new { SchoolId = 2, StudentId = 23, FirstName = "Ketty", LastName = "Oak", Gender = Gender.Male, BirthDay = new DateOnly(2053, 7, 25) },

                // Earth School
                new { SchoolId = 3, StudentId = 30, FirstName = "Mike", LastName = "Wat", Gender = Gender.Male, BirthDay = new DateOnly(1999, 5, 15) },
                new { SchoolId = 3, StudentId = 31, FirstName = "Sam", LastName = "Joshi", Gender = Gender.Male, BirthDay = new DateOnly(2000, 6, 23) },
                new { SchoolId = 3, StudentId = 32, FirstName = "Kerry", LastName = "Travade", Gender = Gender.Female, BirthDay = new DateOnly(2001, 2, 6) },
                new { SchoolId = 3, StudentId = 33, FirstName = "Pett", LastName = "Jay", Gender = Gender.Female, BirthDay = new DateOnly(1998, 11, 7) },

                // Mars School
                new { SchoolId = 4, StudentId = 40, FirstName = "Mike", LastName = "Wat", Gender = Gender.Male, BirthDay = new DateOnly(2011, 11, 15) },
                new { SchoolId = 4, StudentId = 41, FirstName = "Sam", LastName = "Joshi", Gender = Gender.Male, BirthDay = new DateOnly(2023, 6, 6) },
                new { SchoolId = 4, StudentId = 42, FirstName = "Kerry", LastName = "Travade", Gender = Gender.Female, BirthDay = new DateOnly(2039, 5, 13) },

                // Jupiter School
                new { SchoolId = 5, StudentId = 50, FirstName = "David", LastName = "Padron", Gender = Gender.Female, BirthDay = new DateOnly(2065, 12, 3) },
                new { SchoolId = 5, StudentId = 53, FirstName = "Jeh", LastName = "Brook", Gender = Gender.Female, BirthDay = new DateOnly(2034, 10, 15) },
                new { SchoolId = 5, StudentId = 54, FirstName = "Steve", LastName = "Johnson", Gender = Gender.Male, BirthDay = new DateOnly(2015, 3, 2) },

                // Saturn School
                new { SchoolId = 6, StudentId = 60, FirstName = "John", LastName = "Haney", Gender = Gender.Female, BirthDay = new DateOnly(2008, 12, 1) },
                new { SchoolId = 6, StudentId = 61, FirstName = "Morgan", LastName = "Frost", Gender = Gender.Male, BirthDay = new DateOnly(2009, 11, 4) },
                new { SchoolId = 6, StudentId = 62, FirstName = "Jennifer", LastName = "Viles", Gender = Gender.Female, BirthDay = new DateOnly(1989, 3, 15) },

                // Uranus School
                new { SchoolId = 7, StudentId = 72, FirstName = "Matt", LastName = "Dally", Gender = Gender.Male, BirthDay = new DateOnly(2011, 11, 4) },
                new { SchoolId = 7, StudentId = 73, FirstName = "Kevin", LastName = "Vax", Gender = Gender.Male, BirthDay = new DateOnly(2012, 5, 12) },
                new { SchoolId = 7, StudentId = 76, FirstName = "John", LastName = "Clarey", Gender = Gender.Female, BirthDay = new DateOnly(2008, 8, 8) },

                // Neptune School
                new { SchoolId = 8, StudentId = 81, FirstName = "Adam", LastName = "Singh", Gender = Gender.Male, BirthDay = new DateOnly(2006, 6, 23) },
                new { SchoolId = 8, StudentId = 82, FirstName = "Bob", LastName = "Joe", Gender = Gender.Male, BirthDay = new DateOnly(1978, 11, 15) },
                new { SchoolId = 8, StudentId = 84, FirstName = "Martin", LastName = "Dalton", Gender = Gender.Female, BirthDay = new DateOnly(2017, 5, 14) },

                // Pluto School
                new { SchoolId = 9, StudentId = 91, FirstName = "Michael", LastName = "Wu", Gender = Gender.Male, BirthDay = new DateOnly(2022, 9, 22) },
                new { SchoolId = 9, StudentId = 93, FirstName = "Rachel", LastName = "Wottle", Gender = Gender.Female, BirthDay = new DateOnly(2022, 10, 5) },
                new { SchoolId = 9, StudentId = 97, FirstName = "Aakash", LastName = "Aarav", Gender = Gender.Male, BirthDay = new DateOnly(2003, 3, 15) }
            );
    }
}
