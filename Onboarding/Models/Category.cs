using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Onboarding.Models;

public class Category
{
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
}

public class AllCategories
{
    public Category[] Categories { get; set; }
}