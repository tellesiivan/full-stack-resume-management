using backend.Core.Dtos.Jobs;
using backend.Core.Entities;
using backend.Core.Models;

namespace backend.Services.Jobs;

public interface IJobsService
{
    Task<BaseResponse> CreateAJob(JobCreateDto jobCreateDto);
    Task<BaseResponse> DeleteAJobById(long id);
    Task<Response<Job>> GetJobById(long id);
    Task<Response<List<JobResponseDto>>> SearchJobs(JobsSearchQuery searchQuery);
}