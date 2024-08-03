namespace Manager.APIs.Dtos;

public class GroupCreateInput
{
    public DateTime CreatedAt { get; set; }

    public List<Employee>? Employees { get; set; }

    public string? Id { get; set; }

    public DateTime UpdatedAt { get; set; }
}
