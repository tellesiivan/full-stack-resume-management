using backend.Core.Enums;

namespace backend.Core.Dtos.Company;

public  class CompanyResponseDto
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public bool IsActive { get; set; } = true;
    public string Name { get; set; } = string.Empty;
    public CompanySize Size { get; set; }
}