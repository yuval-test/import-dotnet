using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Import.Infrastructure.Models;

[Table("SystemTypes")]
public class SystemTypeDbModel
{
    public List<ClientDbModel>? Client { get; set; } = new List<ClientDbModel>();

    [Required()]
    [StringLength(256)]
    public string Description { get; set; }

    [Key()]
    [Required()]
    public int Id { get; set; }
}
