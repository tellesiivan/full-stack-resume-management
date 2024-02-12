using backend.Core.Dtos.Candidate;
using backend.Core.Models;

namespace backend.Services.Candidate;

public interface ICandidateService
{
    Task<BaseResponse> CreateCandidate(CandidateCreateDto candidateCreateDto, IFormFile pdfFile);
    Task<BaseResponse> DeleteCandidate(long id);

}