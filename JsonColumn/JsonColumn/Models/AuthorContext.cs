using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace JsonColumn.Models
{
    public class AuthorContext : DbContext
    {
        public AuthorContext(DbContextOptions options)
            : base(options)
        {
        }

        public static Array JsonQuery(string expression, string path) => throw new NotImplementedException();

        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().OwnsOne(
                author => author.Contact, ownedNavigationBuilder =>
                {
                    ownedNavigationBuilder.ToJson();
                    ownedNavigationBuilder.OwnsOne(contactDetails => contactDetails.Address);
                });

            modelBuilder.Entity<Author>().OwnsMany(
                author => author.Addresses, ownedNavigationBuilder =>
                {
                    ownedNavigationBuilder.ToJson();
                });
        }

        public async Task Seed()
        {
            var authors = new List<Author>
            {
                new Author("Maddy Montaquila")
                {
                    Contact = new() { Address = new("1 Main St", "Camberwick Green", "CW1 5ZH", "UK"), Phone = "01632 12345" },
                    Addresses = new List<Address>
                    {
                        new("1 Main St", "Camberwick Green", "CW1 5ZH", "UK"),
                        new("8th St", "Red", "123 5ZH", "UK")
                    }
                },
                new Author("Jeremy Likness")
                {
                    Contact = new() { Address = new("2 Main St", "Chigley", "CW1 5ZH", "UK"), Phone = "01632 12346" },
                    Addresses = new List<Address>
                    {
                        new("123 St", "Black", "777", "UK"),
                        new("Black St", "Black", "777", "UK")
                    }
                },
                new Author("Daniel Roth")
                {
                    Contact = new() { Address = new("3 Main St", "Camberwick Green", "CW1 5ZH", "UK"), Phone = "01632 12347" },
                    Addresses = new List<Address>
                    {
                        new("1 St", "Yellow", "CW1", "USA"),
                        new("Love St", "Reeeed", "123", "UK")
                    }
                },
                new Author("Arthur Vickers")
                {
                    Contact = new() { Address = new("15a Main St", "Chigley", "CW1 5ZH", "UK"), Phone = "01632 12348" },
                    Addresses = new List<Address>
                    {
                        new("It Rd", "Redmond", "908029", "USA")
                    }
                },
                new Author("Brice Lambson")
                {
                    Contact = new() { Address = new("4 Main St", "Chigley", "CW1 5ZH", "UK"), Phone = "01632 12349" },
                    Addresses = new List<Address>
                    {
                        new("34 St", "Iure", "5ZH", "USA"),
                        new("12 St", "Aded", "87Z", "UK")
                    }
                }
            };

            await AddRangeAsync(authors);
            await SaveChangesAsync();
        }
    }
}
