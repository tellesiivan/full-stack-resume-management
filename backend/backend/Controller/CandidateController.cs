using System.Net.Mime;
using backend.Core.Dtos.Candidate;
using backend.Core.Models;
using backend.Services.Candidate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controller;

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
        
        [HttpGet]
        public async Task<ActionResult<Response<IEnumerable<CandidatesResponseDto>>>> GetAll()
        {
            var response = await candidateService.GetCandidates();
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        
        [HttpGet("download/{resumeUrl}")]
        public IActionResult DownloadResumePdf(string resumeUrl)
        {
            if (!resumeUrl.EndsWith(".pdf"))
            {
                return BadRequest("The provided url is not a pdf url");
            }
            var filePath =
                Path.Combine(Directory.GetCurrentDirectory(), "documents", "pdfs",
                    resumeUrl);
            
            var pdfBytes = System.IO.File.ReadAllBytes(filePath);
            var file = File(pdfBytes, MediaTypeNames.Application.Pdf, resumeUrl);
            return file;
        }
    }

