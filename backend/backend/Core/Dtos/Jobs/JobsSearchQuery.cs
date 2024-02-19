using backend.Core.Enums;
using backend.Core.Models;

namespace backend.Core.Dtos.Jobs;

public class JobsSearchQuery: BaseSearchQuery
{
    public string? Title { get; set; }
    public JobLevel? JobLevel { get; set; }
    public int? CompanyId { get; set; }
}