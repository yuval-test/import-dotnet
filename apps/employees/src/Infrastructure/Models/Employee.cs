using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employees.Infrastructure.Models;

[Table("Employees")]
public class EmployeeDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? Name { get; set; }

    [StringLength(1000)]
    public string? Phone { get; set; }

    public DateTime? StartDate { get; set; }

    public List<EmployeeDbModel>? Supervisees { get; set; } = new List<EmployeeDbModel>();

    public string? SupervisorId { get; set; }

    [ForeignKey(nameof(SupervisorId))]
    public EmployeeDbModel? Supervisor { get; set; } = null;

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
