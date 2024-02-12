using backend.Core.Dtos.Candidate;
using backend.Core.Models;
using backend.Services.Candidate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController(ICandidateService candidateService) : ControllerBase
    {
        // Create
        [HttpPost("create")]
        public async Task<ActionResult<BaseResponse>> Create([FromForm] CandidateCreateDto candidateCreateDto, IFormFile pdfFile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await candidateService.CreateCandidate(candidateCreateDto, pdfFile);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
