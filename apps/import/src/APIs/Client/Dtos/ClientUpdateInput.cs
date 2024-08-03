namespace Import.APIs.Dtos;

public class ClientUpdateInput
{
    public string? CompanyName { get; set; }

    public DateTime? Created { get; set; }

    public string? Createdby { get; set; }

    public string? DefaultCultureCode { get; set; }

    public string? Id { get; set; }

    public bool? IsSokClient { get; set; }

    public DateTime? Modified { get; set; }

    public string? Modifiedby { get; set; }

    public string? NumberField { get; set; }

    public int? SystemTypeField { get; set; }
}
