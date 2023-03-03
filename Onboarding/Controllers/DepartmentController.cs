using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Onboarding.Models;

namespace Onboarding.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentController : ControllerBase
{
    private readonly ApplicationContext _db;

    public DepartmentController(ApplicationContext db)
    {
        _db = db;
    }
    
    // GET: api/Department
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
    {
        if (_db.Departments == null)
        {
            return NotFound();
        }

        return await _db.Departments.ToListAsync();
    }
    
    // GET: api/Department/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Department>> GetDepartment(int id)
    {
        if (_db.Departments == null)
            return NotFound();

        var department = await _db.Departments.FindAsync(id);

        if (department == null)
            return NotFound();

        return department;
    }
    
    // POST: api/Department
    [HttpPost]
    public async Task<ActionResult<Department>> PostDepartment(Department department)
    {
        if (_db.Departments == null)
        {
            return Problem("Entity set 'ApplicationContext.Departments'  is null.");
        }
        _db.Departments.Add(department);
        await _db.SaveChangesAsync();

        // return CreatedAtAction("GetDepartments", new { id = Department.Id }, Department);
        return NoContent();
    }
    
    // PUT: api/Department/5
    [HttpPut("{id}")]
    public async Task<ActionResult<Department>> PutDepartment(int id, Department department)
    {
        if (id != department.Id)
            return BadRequest("Id mismatch");
        
        _db.Entry(department).State = EntityState.Modified;

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DepartmentExists(id))
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
    
    // DELETE: api/Department/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDepartment(int id)
    {
        var department = await _db.Departments.FindAsync(id);
        if (department == null)
        {
            return NotFound();
        }

        _db.Departments.Remove(department);
        await _db.SaveChangesAsync();

        return NoContent();
    }
    
    private bool DepartmentExists(int id)
    {
        var department = _db.Departments.Find(id);
        return department != null;
    }
}