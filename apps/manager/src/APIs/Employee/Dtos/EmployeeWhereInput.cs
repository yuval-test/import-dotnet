namespace Manager.APIs.Dtos;

public class EmployeeWhereInput
{
    public DateTime? CreatedAt { get; set; }

    public List<string>? Employees { get; set; }

    public string? Id { get; set; }

    public string? Manager { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
