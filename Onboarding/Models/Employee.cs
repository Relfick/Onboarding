using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Onboarding.Models;

public class Employee
{
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public int RoleId { get; set; }
    
    [Required] 
    public int DepartmentId { get; set; }
    
    [Required] 
    public int CityId { get; set; }
    
    public string? PhoneNumber { get; set; } 
    
    public long? TgUserId { get; set; }
    public string? TgUsername { get; set; }
}