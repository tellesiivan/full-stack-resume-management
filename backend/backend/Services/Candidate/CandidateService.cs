using System.Net.Mime;
using System.Text;
using AutoMapper;
using backend.Core.Context;
using backend.Core.Dtos.Candidate;
using backend.Core.Models;
using backend.Services.Jobs;

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
            
            applicationDbContext.Candidates.Add(candidate);
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
}