namespace Import.APIs.Dtos;

public class UserCreateInput
{
    public string ClientId { get; set; }

    public DateTime Created { get; set; }

    public string Createdby { get; set; }

    public string? Email { get; set; }

    public string? Id { get; set; }

    public bool IsDisabled { get; set; }

    public bool IsMainUser { get; set; }

    public DateTime? Modified { get; set; }

    public string? Modifiedby { get; set; }

    public string? TrustedPhoneNumber { get; set; }
}
