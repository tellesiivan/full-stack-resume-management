using backend.Core.Dtos.Company;
using backend.Core.Entities;
using backend.Core.Models;
using backend.Services.Company;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController(ICompanyService companyService) : ControllerBase
    {
        // Create
        [HttpPost("create")]
        public async Task<ActionResult<Response<string>>> Create([FromBody] CompanyCreationDto companyCreationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await companyService.CreateCompany(companyCreationDto);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        
        // Search All
        [HttpGet("search")]
        public async Task<ActionResult<Response<List<CompanyResponseDto>>>> SearchAll([FromQuery] CompanySearchQuery searchQuery)
        {
            var response = await companyService.SearchCompanies(searchQuery);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        
        // Get Company by id
        [HttpGet("{id:long}")]
        public async Task<ActionResult<Response<List<CompanyResponseDto>>>> SearchById([FromRoute] long id)
        {
            var response = await companyService.GetCompanyById(id);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        
        // Delete Company by id
        [HttpDelete("delete/{id:long}")]
        public async Task<ActionResult<BaseResponse>> DeleteById([FromRoute] long id)
        {
            var response = await companyService.DeleteCompanyById(id);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        
        // // Update
        // [HttpPut("update/{id:int}")]
        // public IActionResult Update(int id)
        // {
        //     // code for updating a company by id
        // }
        // // Delete
        // [HttpDelete("delete/{id:int}")]
        // public IActionResult Delete(int id)
        // {
        //     // code for deleting a company by id
        // }
    }
}
