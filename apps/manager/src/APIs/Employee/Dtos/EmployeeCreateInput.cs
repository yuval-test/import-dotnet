namespace Manager.APIs.Dtos;

public class EmployeeCreateInput
{
    public DateTime CreatedAt { get; set; }

    public List<Employee>? Employees { get; set; }

    public string? Id { get; set; }

    public Employee? Manager { get; set; }

    public DateTime UpdatedAt { get; set; }
}