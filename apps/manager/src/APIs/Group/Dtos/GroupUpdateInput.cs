namespace Manager.APIs.Dtos;

public class GroupUpdateInput
{
    public DateTime? CreatedAt { get; set; }

    public List<string>? Employees { get; set; }

    public string? Id { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
