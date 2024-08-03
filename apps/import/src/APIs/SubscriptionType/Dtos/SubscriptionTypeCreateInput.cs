namespace Import.APIs.Dtos;

public class SubscriptionTypeCreateInput
{
    public List<Contract>? Contract { get; set; }

    public string Description { get; set; }

    public int? Id { get; set; }
}
