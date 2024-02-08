using System.Net;
using AutoMapper;
using backend.Core.Context;
using backend.Core.Dtos.Jobs;
using backend.Core.Entities;
using backend.Core.Models;

namespace backend.Services.Jobs;

public class JobsService(ApplicationDbContext applicationDbContext, IMapper mapper) : IJobsService
{
    public async Task<BaseResponse> CreateAJob(JobCreateDto jobCreateDto)
    {
        var response = new BaseResponse();
        try
        {
            var newJob = mapper.Map<Job>(jobCreateDto);
            applicationDbContext.Jobs.Add(newJob);
            await applicationDbContext.SaveChangesAsync();
            response.Message = "Job created successfully";
        }
        catch (Exception e)
        {
            response.Message = "Unable to create job at this time";
            response.IsSuccess = false;
        }

        return response;
       
    }

    public async Task<BaseResponse> DeleteAJobById(long id)
    {
        var response = new BaseResponse();

        try
        {
            var matchedJob = await FindMatchedJob(id);
            if (matchedJob == null)
            {
                throw new Exception("Job not found");
                
            }
            applicationDbContext.Jobs.Remove(matchedJob);
            await applicationDbContext.SaveChangesAsync();
            response.Message = "Job deleted successfully";
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.IsSuccess = false;
        }

        return response;
    }

    public async Task<Response<JobResponseDto>> GetJobById(long id)
    {
        var response = new Response<JobResponseDto>();
        try
        {
            var jobMatched = await FindMatchedJob(id);
            if (jobMatched is null)
            {
                throw new Exception("Job with the provided id does not exist");
            }
            response.Data = mapper.Map<JobResponseDto>(jobMatched);
            response.StatusCode = HttpStatusCode.OK;
        }
        catch (Exception e)
        {
            response.IsSuccess = false;
            response.ErrorMessage = e.Message;
        }
        return response;
    }

    public async Task<Response<List<JobResponseDto>>> SearchJobs(JobsSearchQuery searchQuery)
    {
        throw new NotImplementedException();
    }


    private async Task<Job?> FindMatchedJob(long id) => await applicationDbContext.Jobs.FindAsync(id);
}