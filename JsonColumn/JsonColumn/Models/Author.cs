using Microsoft.Extensions.Hosting;

namespace JsonColumn.Models;

public class Author
{
    public Author(string name)
    {
        Name = name;
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public ContactDetails Contact { get; set; } = null!;

    public IList<Address> Addresses { get; set; }
}
