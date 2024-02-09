using backend.Core.Enums;

namespace backend.Core.Entities;

public class Job: BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public JobLevel JobLevel { get; set; }
    
    // relations
    public long CompanyId { get; set; }
    public Company? Company { get; set; }
    
    // candidate relation
    public ICollection<Candidate>? Candidates { get; set; }
}