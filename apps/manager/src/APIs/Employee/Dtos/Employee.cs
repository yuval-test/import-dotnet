namespace Manager.APIs.Dtos;

public class Employee
{
    public DateTime CreatedAt { get; set; }

    public List<string>? Employees { get; set; }

    public List<string>? Groups { get; set; }

    public string Id { get; set; }

    public string? Manager { get; set; }

    public DateTime UpdatedAt { get; set; }
}
