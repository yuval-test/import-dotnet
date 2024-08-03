namespace Import.APIs.Dtos;

public class ContractUpdateInput
{
    public string? ClientId { get; set; }

    public DateTime? Created { get; set; }

    public string? Createdby { get; set; }

    public DateTime? ExpireDate { get; set; }

    public string? Id { get; set; }

    public DateTime? Modified { get; set; }

    public string? Modifiedby { get; set; }

    public int? RealtedSubscriptionType { get; set; }

    public DateTime? StartDate { get; set; }
}
