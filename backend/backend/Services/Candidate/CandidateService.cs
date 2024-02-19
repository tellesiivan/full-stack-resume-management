using System.Net.Mime;
using System.Text;
using AutoMapper;
using backend.Core.Context;
using backend.Core.Dtos.Candidate;
using backend.Core.Models;
using backend.Services.Jobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Candidate;

public class CandidateService(IJobsService jobsService, ApplicationDbContext applicationDbContext, IMapper mapper): ICandidateService
{
    public async Task<BaseResponse> CreateCandidate(CandidateCreateDto candidateCreateDto, IFormFile pdfFile)
    {
        var response = new BaseResponse();

        try
        {

            var fiveMegaByte = 5 * 1024 * 1024;
            var pdfMimeType = MediaTypeNames.Application.Pdf;

            if (pdfFile.ContentType != pdfMimeType || pdfFile.Length > fiveMegaByte)
            {
                throw new Exception("Invalid Pdf file type, file is either not a pdf of its over 5mb");
            }

            var stringBuilder = new StringBuilder(Guid.NewGuid().ToString());
            var resumeUrl = stringBuilder.Append(".pdf").ToString();

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "documents", "pdfs",
                resumeUrl);

            // save pdf file to server
            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await pdfFile.CopyToAsync(stream);
            }
            
            
            var matchedJobResponse = await jobsService.GetJobById(candidateCreateDto.JobId);
            if (!matchedJobResponse.IsSuccess)
            {
                throw new Exception(matchedJobResponse.ErrorMessage);
            }

            var candidate = mapper.Map<Core.Entities.Candidate>(candidateCreateDto);
            // Save our file url to our entity
            candidate.ResumeUrl = resumeUrl;
            
            await applicationDbContext.Candidates.AddAsync(candidate);
            await applicationDbContext.SaveChangesAsync();
            
            response.Message = "Candidate created successfully";
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.IsSuccess = false;
        }

        return response;
    }

    public async Task<BaseResponse> DeleteCandidate(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<CandidatesResponseDto>> GetCandidateById(long id)
    {
        var response = new Response<CandidatesResponseDto>();
        return response;
    }

    public async Task<Response<IEnumerable<CandidatesResponseDto>>> GetCandidates()
    {
        var response = new Response<IEnumerable<CandidatesResponseDto>>();
        try
        {
            var candidates = await applicationDbContext.Candidates
                // .Include(candidate =>
                //     candidate.Job)
                .ToListAsync();

            var convertedCandidates = mapper.Map<IEnumerable<CandidatesResponseDto>>(candidates);
            response.Data = convertedCandidates; 
        }
        catch (Exception e)
        {
            response.ErrorMessage = e.Message;
            response.IsSuccess = false;
        }

        return response;
    }
}