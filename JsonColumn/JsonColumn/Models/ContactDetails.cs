using Microsoft.Extensions.Hosting;
using System.Net;

namespace JsonColumn.Models;

public class ContactDetails
{
    public Address Address { get; set; } = null!;

    public string Phone { get; set; }
}
