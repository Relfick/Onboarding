using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Onboarding.Models;

namespace Onboarding.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly ApplicationContext _db;

    public EmployeeController(ApplicationContext db)
    {
        _db = db;
    }
    
    // GET: api/Employee
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
    {
        if (_db.Employees == null)
        {
            return NotFound();
        }
        
        var employees = await _db.Employees
            .Include(a => a.City)
            .Include(a => a.Department)
            .Include(a => a.Role)
            .ToListAsync();
    
        return employees;
    }
    
    [HttpPost]
    public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
    {
        if (_db.Employees == null)
        {
            return Problem("Entity set 'ApplicationContext.Employees'  is null.");
        }

        employee.CityId = employee.City.Id;
        employee.City = null;
        employee.DepartmentId = employee.Department.Id;
        employee.Department = null;
        employee.RoleId = employee.Role.Id;
        employee.Role = null;
        _db.Employees.Add(employee);
        await _db.SaveChangesAsync();

        // return CreatedAtAction("GetArticle", new { id = Article.Id }, Article);
        return NoContent();
    }
}