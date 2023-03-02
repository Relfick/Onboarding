using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Onboarding.Models;

public class Employee
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }

    public int RoleId { get; set; }
    [ForeignKey("RoleId")]
    public Role Role { get; set; }
    
    public int DepartmentId { get; set; }
    [ForeignKey("DepartmentId")]
    public Department Department { get; set; }
    
    public int CityId { get; set; }
    [ForeignKey("CityId")]
    public City City { get; set; } 
    
    public string? PhoneNumber { get; set; } 
    
    public long? TgUserId { get; set; }
    public string? TgUsername { get; set; }
}