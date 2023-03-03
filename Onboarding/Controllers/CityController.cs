using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Onboarding.Models;

namespace Onboarding.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CityController : ControllerBase
{
    private readonly ApplicationContext _db;

    public CityController(ApplicationContext db)
    {
        _db = db;
    }
    
    // GET: api/City
    [HttpGet]
    public async Task<ActionResult<IEnumerable<City>>> GetCities()
    {
        if (_db.Cities == null)
        {
            return NotFound();
        }

        return await _db.Cities.ToListAsync();
    }
    
    // GET: api/City/5
    [HttpGet("{id}")]
    public async Task<ActionResult<City>> GetCity(int id)
    {
        if (_db.Cities == null)
            return NotFound();

        var city = await _db.Cities.FindAsync(id);

        if (city == null)
            return NotFound();

        return city;
    }
    
    // POST: api/City
    [HttpPost]
    public async Task<ActionResult<City>> PostCity(City city)
    {
        if (_db.Cities == null)
        {
            return Problem("Entity set 'ApplicationContext.Cities'  is null.");
        }
        _db.Cities.Add(city);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCity), new { id = city.Id, name = city.Name }, city);
    }
    
    // PUT: api/City/5
    [HttpPut("{id}")]
    public async Task<ActionResult<City>> PutCity(int id, City city)
    {
        if (id != city.Id)
            return BadRequest("Id mismatch");
        
        _db.Entry(city).State = EntityState.Modified;

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CityExists(id))
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
    
    // DELETE: api/City/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCity(int id)
    {
        var city = await _db.Cities.FindAsync(id);
        if (city == null)
        {
            return NotFound();
        }

        _db.Cities.Remove(city);
        await _db.SaveChangesAsync();

        return NoContent();
    }
    
    private bool CityExists(int id)
    {
        var city = _db.Cities.Find(id);
        return city != null;
    }
}