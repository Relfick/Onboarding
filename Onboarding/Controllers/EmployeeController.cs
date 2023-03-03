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
        
        var employees = await _db.Employees.ToListAsync();
    
        return employees;
    }
    
    // GET: api/Employee/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> GetEmployee(int id)
    {
        if (_db.Employees == null)
            return NotFound();

        var employee = await _db.Employees.FindAsync(id);

        if (employee == null)
            return NotFound();

        return employee;
    }
    
    // POST: api/Employee
    [HttpPost]
    public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
    {
        if (_db.Employees == null)
        {
            return Problem("Entity set 'ApplicationContext.Categories'  is null.");
        }
        _db.Employees.Add(employee);
        await _db.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetEmployee), 
            new
            {
                id = employee.Id, 
                name = employee.Name, 
                roleId = employee.RoleId,
                departmentId = employee.DepartmentId,
                cityId = employee.CityId,
                phoneNumber = employee.PhoneNumber,
                tgUserId = employee.TgUserId,
                tgUsername = employee.TgUsername
            }, 
            employee);
    }
    
    // PUT: api/Employee/5
    [HttpPut("{id}")]
    public async Task<ActionResult<Employee>> PutEmployee(int id, Employee employee)
    {
        if (id != employee.Id)
            return BadRequest("Id mismatch");
        
        _db.Entry(employee).State = EntityState.Modified;

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EmployeeExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        
        return NoContent();
    }
    
    // DELETE: api/Employee/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var employee = await _db.Employees.FindAsync(id);
        if (employee == null)
        {
            return NotFound();
        }

        _db.Employees.Remove(employee);
        await _db.SaveChangesAsync();

        return NoContent();
    }
    
    private bool EmployeeExists(int id)
    {
        var employee = _db.Employees.Find(id);
        return employee != null;
    }
}