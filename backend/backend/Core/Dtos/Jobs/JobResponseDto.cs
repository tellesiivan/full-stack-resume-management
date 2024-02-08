using backend.Core.Entities;
using backend.Core.Enums;

namespace backend.Core.Dtos.Jobs;

public class JobResponseDto
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public JobLevel JobLevel { get; set; }
    public long CompanyId { get; set; }
    public string CompanyName { get; set; } = string.Empty;

}