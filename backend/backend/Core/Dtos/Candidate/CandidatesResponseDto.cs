namespace backend.Core.Dtos.Candidate;

public class CandidatesResponseDto
{
    public long Id { get; set; }
    
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string CoverLetter { get; set; } = string.Empty;
    public string ResumeUrl { get; set; } = string.Empty;
    public long JobId { get; set; }
    public string JobTitle { get; set; } = string.Empty;
}