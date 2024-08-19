namespace Employees.APIs.Dtos;

public class EmployeeWhereInput
{
    public DateTime? CreatedAt { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public DateTime? StartDate { get; set; }

    public List<string>? Supervisees { get; set; }

    public string? Supervisor { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
