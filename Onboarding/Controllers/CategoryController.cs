using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Onboarding.Models;

namespace Onboarding.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ApplicationContext _db;

    public CategoryController(ApplicationContext db)
    {
        _db = db;
    }

    // GET: api/Category
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        if (_db.Categories == null)
        {
            return NotFound();
        }

        return await _db.Categories.ToListAsync();
    }
    
    // GET: api/Category
    [HttpGet]
    [Route("~/api/tg/category")]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategoriesTg([FromHeader] TelegramCredentials tgCredentials)
    {
        if (_db.Categories == null)
        {
            return NotFound();
        }

        if (tgCredentials == null)
        {
            return NotFound();
        }

        var userExists = _db.Employees.Any(e => e.TgUserId == tgCredentials.Id);

        if (!userExists)
        {
            return NotFound();
        }

        return await GetCategories();
    } 
    

    // GET: api/Category/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategory(int id)
    {
        if (_db.Categories == null)
            return NotFound();

        var category = await _db.Categories.FindAsync(id);

        if (category == null)
            return NotFound();

        return category;
    }
    
    // POST: api/Category
    [HttpPost]
    public async Task<ActionResult<Category>> PostCategory(Category category)
    {
        if (_db.Categories == null)
        {
            return Problem("Entity set 'ApplicationContext.Categories'  is null.");
        }
        _db.Categories.Add(category);
        await _db.SaveChangesAsync();

        // return CreatedAtAction(nameof(GetById), new { id = Category.Id }, Category);
        return NoContent();
    }
    
    // POST: api/Categories
    [HttpPost]
    [Route("~/api/Categories")]
    public async Task<ActionResult<AllCategories>> PostCategories(AllCategories allCategories)
    {
        if (_db.Categories == null)
        {
            return Problem("Entity set 'ApplicationContext.Categories'  is null.");
        }

        var categories = allCategories.Categories;
        _db.Categories.AddRange(categories);
        await _db.SaveChangesAsync();

        // return CreatedAtAction("GetCategories", new { id = Category.Id }, Category);
        return NoContent();
    }
}