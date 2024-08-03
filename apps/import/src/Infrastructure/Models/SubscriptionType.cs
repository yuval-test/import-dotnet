using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Import.Infrastructure.Models;

[Table("SubscriptionTypes")]
public class SubscriptionTypeDbModel
{
    public List<ContractDbModel>? Contract { get; set; } = new List<ContractDbModel>();

    [Required()]
    [StringLength(256)]
    public string Description { get; set; }

    [Key()]
    [Required()]
    public int Id { get; set; }
}
