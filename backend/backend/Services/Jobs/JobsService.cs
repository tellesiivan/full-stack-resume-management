using System.Net;
using AutoMapper;
using backend.Core.Context;
using backend.Core.Dtos.Jobs;
using backend.Core.Entities;
using backend.Core.Models;
using backend.Services.Company;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Jobs;

public class JobsService(ApplicationDbContext applicationDbContext, IMapper mapper, ICompanyService companyService) : IJobsService
{
    public async Task<BaseResponse> CreateAJob(JobCreateDto jobCreateDto)
    {
        var response = new BaseResponse();
        try
        {
            var companyResponse = await companyService.GetCompanyById(jobCreateDto.CompanyId);
            if (companyResponse?.Data is null)
            {
                throw new Exception("Company not found");
            }
            
            var newJob = mapper.Map<Job>(jobCreateDto);
            // add this due to the foreign key relationship 
            newJob.CompanyId = companyResponse.Data.Id;
            
            await applicationDbContext.Jobs.AddAsync(newJob);
            await applicationDbContext.SaveChangesAsync();
            response.Message = "Job created successfully";
        }
        catch (Exception e)
        {
            response.Message =e.Message;
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

    public async Task<Response<Job>> GetJobById(long id)
    {
        var response = new Response<Job>();
        try
        {
            var jobMatched = await FindMatchedJob(id);
            if (jobMatched is null)
            {
                throw new Exception("Job with the provided id does not exist");
            }

            response.Data = jobMatched;
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
        var response = new Response<List<JobResponseDto>>();
        try
        {
            var queryableJobs = applicationDbContext.Jobs
                .Include(job => job.Candidates)
                .Include(job => job.Company)
                .AsQueryable();
            
            if (searchQuery.JobLevel.HasValue)
            {
                queryableJobs = queryableJobs.Where(job => job.JobLevel == searchQuery.JobLevel);
            }

            if (!string.IsNullOrEmpty(searchQuery.Title))
            {
                queryableJobs = queryableJobs.Where(job => job.Title.Contains(searchQuery.Title));
            }

            queryableJobs = searchQuery.IsDescending
                ? queryableJobs.OrderByDescending(job => job.Title)
                : queryableJobs.OrderBy(job => job.Title);

            var skipSize = (searchQuery.PageNumber - 1) * searchQuery.PageSize;
            var jobsList = await queryableJobs.Skip(skipSize).Take(searchQuery.PageSize).ToListAsync();
            
            response.Data = jobsList.Select(mapper.Map<JobResponseDto>).ToList();
            response.StatusCode = HttpStatusCode.OK;

        }
        catch (Exception e)
        {
            response.IsSuccess = true;
            response.ErrorMessage = e.Message;
        }

        return response;

    }


    private async Task<Job?> FindMatchedJob(long id) => await applicationDbContext.Jobs
        .Include(job => job.Candidates)
        .FirstOrDefaultAsync(job => job.Id == id);
}