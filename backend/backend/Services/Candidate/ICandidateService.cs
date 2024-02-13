using backend.Core.Dtos.Candidate;
using backend.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Services.Candidate;

public interface ICandidateService
{
    Task<BaseResponse> CreateCandidate(CandidateCreateDto candidateCreateDto, IFormFile pdfFile);
    Task<BaseResponse> DeleteCandidate(long id);
    Task<Response<CandidatesResponseDto>> GetCandidateById(long id);
    Task<Response<IEnumerable<CandidatesResponseDto>>> GetCandidates();

}