using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Onboarding.Models;

namespace Onboarding.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArticleController : ControllerBase
{
    private readonly ApplicationContext _db;

    public ArticleController(ApplicationContext db)
    {
        _db = db;
    }

    // GET: api/Article
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Article>>> GetArticles()
    {
        if (_db.Articles == null)
        {
            return NotFound();
        }
        
        var articles = await _db.Articles.ToListAsync();

        return articles;
    }

    // GET: api/Article/5
    [HttpGet("{id}")]
    public async Task<ActionResult<NWArticle>> GetArticle(int id)
    {
        if (_db.Articles == null)
            return NotFound();

        var article = _db.Articles.Find(id);

        if (article == null)
            return NotFound();

        var nwArticle = new NWArticle
        {
            Id = article.Id,
            Text = article.Text,
            Title = article.Title,
            Category = (await _db.Categories.FindAsync(article.CategoryId))!
        };

        return nwArticle;
    }
    
    // GET: api/Article/search/?category
    [HttpGet]
    [Route("search")]
    public async Task<ActionResult<List<NWArticle>>> SearchArticles([FromQuery] int category)
    {
        if (_db.Articles == null)
            return NotFound();

        var articles = await _db.Articles
            .Where(a => a.CategoryId == category)
            .ToListAsync();

        if (articles.Count == 0)
            return NotFound();

        var nwArticles = articles
            .Select(a => new NWArticle
            {
                Id = a.Id,
                Text = a.Text,
                Title = a.Title,
                Category = _db.Categories.Find(a.CategoryId)!
            })
            .ToList();

        return nwArticles;
    }

    // POST: api/Article
    [HttpPost]
    public async Task<ActionResult<Article>> PostArticle(Article article)
    {
        if (_db.Articles == null)
        {
            return Problem("Entity set 'ApplicationContext.Articles'  is null.");
        }

        _db.Articles.Add(article);
        await _db.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetArticle), 
            new { id = article.Id, title = article.Title, text = article.Text, categoryId = article.CategoryId }, 
            article);
    }
    
    // POST: api/Articles
    [HttpPost]
    [Route("~/api/Articles")]
    public async Task<ActionResult<AllArticles>> PostArticles(AllArticles allArticles)
    {
        if (_db.Articles == null)
        {
            return Problem("Entity set 'ApplicationContext.Articles'  is null.");
        }

        var articles = allArticles.Articles;
        _db.Articles.AddRange(articles);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetArticles), allArticles);
    }

    // PUT: api/Article/5
    [HttpPut("{id}")]
    public async Task<ActionResult<Article>> PutArticle(int id, Article article)
    {
        if (id != article.Id)
            return BadRequest("Id mismatch");
        
        _db.Entry(article).State = EntityState.Modified;

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ArticleExists(id))
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
    
    // DELETE: api/Article/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteArticle(int id)
    {
        var article = await _db.Articles.FindAsync(id);
        if (article == null)
        {
            return NotFound();
        }

        _db.Articles.Remove(article);
        await _db.SaveChangesAsync();

        return NoContent();
    }

    private bool ArticleExists(int id)
    {
        var article = _db.Articles.Find(id);
        return article != null;
    }
}