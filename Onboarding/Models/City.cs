using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Onboarding.Models;

public class City
{
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
}