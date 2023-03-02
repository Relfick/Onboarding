using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Onboarding.Models;

public class Department
{
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }

    [JsonIgnore]
    public List<Employee> Employees { get; set; } = new();
}