using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manager.Infrastructure.Models;

[Table("Employees")]
public class EmployeeDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    public List<EmployeeDbModel>? Employees { get; set; } = new List<EmployeeDbModel>();

    public List<GroupDbModel>? Groups { get; set; } = new List<GroupDbModel>();

    [Key()]
    [Required()]
    public string Id { get; set; }

    public string? ManagerId { get; set; }

    [ForeignKey(nameof(ManagerId))]
    public EmployeeDbModel? Manager { get; set; } = null;

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
