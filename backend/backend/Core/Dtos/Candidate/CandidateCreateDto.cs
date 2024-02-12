namespace backend.Core.Dtos.Candidate;

public class CandidateCreateDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string CoverLetter { get; set; } = string.Empty;
    public long JobId { get; set; }
}