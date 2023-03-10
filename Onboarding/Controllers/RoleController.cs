using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Onboarding.Models;

namespace Onboarding.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly ApplicationContext _db;

    public RoleController(ApplicationContext db)
    {
        _db = db;
    }
    
    // GET: api/Role
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
    {
        if (_db.Roles == null)
        {
            return NotFound();
        }

        return await _db.Roles.ToListAsync();
    }
    
    // GET: api/Role/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Role>> GetRole(int id)
    {
        if (_db.Roles == null)
            return NotFound();

        var role = await _db.Roles.FindAsync(id);

        if (role == null)
            return NotFound();

        return role;
    }
    
    // POST: api/Role
    [HttpPost]
    public async Task<ActionResult<Role>> PostRole(Role role)
    {
        if (_db.Roles == null)
        {
            return Problem("Entity set 'ApplicationContext.Roles'  is null.");
        }
        _db.Roles.Add(role);
        await _db.SaveChangesAsync();

        // return CreatedAtAction("GetRoles", new { id = Role.Id }, Role);
        return NoContent();
    }
    
    // PUT: api/Role/5
    [HttpPut("{id}")]
    public async Task<ActionResult<Category>> PutRole(int id, Role role)
    {
        if (id != role.Id)
            return BadRequest("Id mismatch");
        
        _db.Entry(role).State = EntityState.Modified;

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!RoleExists(id))
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
    
    // DELETE: api/Role/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRole(int id)
    {
        var role = await _db.Roles.FindAsync(id);
        if (role == null)
        {
            return NotFound();
        }

        _db.Roles.Remove(role);
        await _db.SaveChangesAsync();

        return NoContent();
    }
    
    private bool RoleExists(int id)
    {
        var role = _db.Roles.Find(id);
        return role != null;
    }
}