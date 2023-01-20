using System.Text.Json;

namespace OData.WebApi.Models;

public class School
{
    public int SchoolId { get; set; }

    public string SchoolName { get; set; }

    public Address MailAddress { get; set; }

    // It's not for Edm model, let's ignore when Edm model building
    public string Emails { get; set; }

    public IList<string> ContactEmails
    {
        get
        {
            return Emails is null ? new List<string>() : JsonSerializer.Deserialize<IList<string>>(Emails);
        }
        set
        {
            Emails = value is null ? string.Empty : JsonSerializer.Serialize(value);
        }
    }

    // It's not for Edm model, let's ignore when Edm model building
    public string Branches { get; set; }

    public IList<Address> BranchAddresses { get; set; }

    public IList<Student> Students { get; set; }
}
