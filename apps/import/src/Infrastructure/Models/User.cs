using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Import.Infrastructure.Models;

[Table("Users")]
public class UserDbModel
{
    [Required()]
    [StringLength(256)]
    public string ClientId { get; set; }

    [Required()]
    public DateTime Created { get; set; }

    [Required()]
    [StringLength(256)]
    public string Createdby { get; set; }

    [StringLength(256)]
    public string? Email { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [Required()]
    public bool IsDisabled { get; set; }

    [Required()]
    public bool IsMainUser { get; set; }

    public DateTime? Modified { get; set; }

    [StringLength(256)]
    public string? Modifiedby { get; set; }

    [StringLength(256)]
    public string? TrustedPhoneNumber { get; set; }
}
