using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Onboarding.Models;

public class Article
{
    public int Id { get; set; }
    
    [Required]
    public string Title { get; set; }
    [Required]
    public string Text { get; set; }
    
    // [Required]
    public int CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public Category Category { get; set; }
}

public class AllArticles
{
    public Article[] Articles { get; set; }
}