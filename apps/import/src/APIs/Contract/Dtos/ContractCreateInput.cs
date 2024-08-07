namespace Import.APIs.Dtos;

public class ContractCreateInput
{
    public string ClientId { get; set; }

    public DateTime Created { get; set; }

    public string Createdby { get; set; }

    public DateTime? ExpireDate { get; set; }

    public string? Id { get; set; }

    public DateTime? Modified { get; set; }

    public string? Modifiedby { get; set; }

    public SubscriptionType? OtherSubscriptionType { get; set; }

    public SubscriptionType RealtedSubscriptionType { get; set; }

    public DateTime StartDate { get; set; }
}
