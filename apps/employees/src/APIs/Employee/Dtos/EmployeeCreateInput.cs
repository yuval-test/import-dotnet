namespace Employees.APIs.Dtos;

public class EmployeeCreateInput
{
    public DateTime CreatedAt { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public DateTime? StartDate { get; set; }

    public List<Employee>? Supervisees { get; set; }

    public Employee? Supervisor { get; set; }

    public DateTime UpdatedAt { get; set; }
}
