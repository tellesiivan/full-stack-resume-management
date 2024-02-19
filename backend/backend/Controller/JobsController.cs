using backend.Core.Dtos.Jobs;
using backend.Core.Entities;
using backend.Core.Models;
using backend.Services.Jobs;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController(IJobsService jobsService) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<ActionResult<BaseResponse>> CreateJob(JobCreateDto jobCreateDto)
        {
            if(!ModelState.IsValid) return  BadRequest(ModelState);
            
            var response = await jobsService.CreateAJob(jobCreateDto);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        
        [HttpDelete("delete/{id:long}")]
        public async Task<ActionResult<BaseResponse>> DeleteJobById([FromRoute] long id) 
        {
            var response = await jobsService.DeleteAJobById(id);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        
        [HttpGet("{id:long}")]
        public async Task<ActionResult<Response<JobResponseDto>>> GetJobById([FromRoute] long id) 
        {
            var response = await jobsService.GetJobById(id);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        
        [HttpGet("search")]
        public async Task<ActionResult<Response<List<JobResponseDto>>>> SearchJobs([FromQuery] JobsSearchQuery searchQuery) 
        {
            var response = await jobsService.SearchJobs(searchQuery);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
