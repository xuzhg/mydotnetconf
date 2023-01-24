using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using OData.WebApi.Models;

namespace OData.WebApi.Controllers;

[Route("odata")]
public class SchoolStudentController : ODataController
{
    private readonly ApplicationDbContext _context;

    public SchoolStudentController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("students")]
    [HttpGet("students/$count")]
    [EnableQuery(PageSize = 2)]
    public IActionResult GetStudents()
    {
        return Ok(_context.Students);
    }

    // [HttpGet("students({id})")] comment out intentionally
    [HttpGet("students/{id}")]
    [EnableQuery]
    public IActionResult GetStudent(int id)
    {
        Student student = _context.Students.FirstOrDefault(c => c.StudentId == id);
        if (student is null)
        {
            return NotFound($"Cannot find the student with Id '{id}'");
        }

        return Ok(student);
    }

    [HttpPost("schools({schoolId})/students")]
    public IActionResult Post(int schoolId, [FromBody] Student student)
    {
        /*
        School school = _context.Schools.Include(c => c.Students)
            .FirstOrDefault(s => s.SchoolId == schoolId);
        */
        _context.Students.Add(student);
        student.SchoolId = schoolId;

       //school.Students.Add(student);
        _context.SaveChanges();
        return Created(student);
    }
}