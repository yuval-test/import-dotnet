namespace Import.APIs.Dtos;

public class SubscriptionTypeUpdateInput
{
    public List<string>? Contract { get; set; }

    public string? Description { get; set; }

    public int? Id { get; set; }

    public List<string>? OtherContracts { get; set; }
}
