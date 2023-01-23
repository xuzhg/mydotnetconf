using JsonColumn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace JsonColumn.Controllers
{
    public class AuthorsController : ControllerBase
    {
        public AuthorsController(AuthorContext context)
        {
            Context = context;
        }

        public AuthorContext Context { get; }

        [HttpGet]
        [EnableQuery]
        public IActionResult Get()
        {
            var authorsInChigley = Context.Authors.AsEnumerable().Where(a => a.Addresses.Count == 2);
            foreach (var author in authorsInChigley)
            {

            }

            return Ok(Context.Authors);
        }
    }
}