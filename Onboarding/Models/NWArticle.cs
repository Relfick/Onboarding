namespace Onboarding.Models;

public class NWArticle
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    public string Text { get; set; }
    
    public Category Category { get; set; }
}