using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Import.Infrastructure.Models;

[Table("Clients")]
public class ClientDbModel
{
    [StringLength(256)]
    public string? CompanyName { get; set; }

    [Required()]
    public DateTime Created { get; set; }

    [Required()]
    [StringLength(256)]
    public string Createdby { get; set; }

    [StringLength(256)]
    public string? DefaultCultureCode { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public bool? IsSokClient { get; set; }

    public DateTime? Modified { get; set; }

    [StringLength(256)]
    public string? Modifiedby { get; set; }

    [StringLength(256)]
    public string? NumberField { get; set; }

    public int? SystemTypeFieldId { get; set; }

    [ForeignKey(nameof(SystemTypeFieldId))]
    public SystemTypeDbModel? SystemTypeField { get; set; } = null;
}
