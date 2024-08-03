using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Import.Infrastructure.Models;

[Table("Contracts")]
public class ContractDbModel
{
    [Required()]
    [StringLength(256)]
    public string ClientId { get; set; }

    [Required()]
    public DateTime Created { get; set; }

    [Required()]
    [StringLength(256)]
    public string Createdby { get; set; }

    public DateTime? ExpireDate { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public DateTime? Modified { get; set; }

    [StringLength(256)]
    public string? Modifiedby { get; set; }

    public int? OtherSubscriptionTypeId { get; set; }

    [ForeignKey(nameof(OtherSubscriptionTypeId))]
    public SubscriptionTypeDbModel? OtherSubscriptionType { get; set; } = null;

    public int RealtedSubscriptionTypeId { get; set; }

    [ForeignKey(nameof(RealtedSubscriptionTypeId))]
    public SubscriptionTypeDbModel RealtedSubscriptionType { get; set; } = null;

    [Required()]
    public DateTime StartDate { get; set; }
}
