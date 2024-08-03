using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manager.Infrastructure.Models;

[Table("Groups")]
public class GroupDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    public List<EmployeeDbModel>? Employees { get; set; } = new List<EmployeeDbModel>();

    [Key()]
    [Required()]
    public string Id { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
