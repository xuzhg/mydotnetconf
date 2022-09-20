using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using OData.WebApi.Models;

namespace OData.WebApi.Controllers
{
    public class SchoolsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SchoolsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_context.Schools);
        }
    }
}