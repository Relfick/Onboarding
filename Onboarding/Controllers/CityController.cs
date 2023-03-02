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
    
    [HttpPost]
    public async Task<ActionResult<City>> PostCity(City city)
    {
        if (_db.Cities == null)
        {
            return Problem("Entity set 'ApplicationContext.Citys'  is null.");
        }
        _db.Cities.Add(city);
        await _db.SaveChangesAsync();

        // return CreatedAtAction("GetCities", new { id = city.Id }, city);
        return NoContent();
    }
}