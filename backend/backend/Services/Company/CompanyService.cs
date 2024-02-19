using System.Net;
using AutoMapper;
using backend.Core.Context;
using backend.Core.Dtos.Company;
using backend.Core.Dtos.Jobs;
using backend.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Company;

public class CompanyService(ApplicationDbContext applicationDbContext, IMapper mapper): ICompanyService
{
    public async Task<Response<string>> CreateCompany(CompanyCreationDto companyCreationDto)
    {
        var response = new Response<string>();
        try
        {
            var newCompany = mapper.Map<Core.Entities.Company>(companyCreationDto);
            await applicationDbContext.Companies.AddAsync(newCompany);
            await applicationDbContext.SaveChangesAsync();
            response.StatusCode = HttpStatusCode.Created;
        }
        catch (Exception e)
        {
            response.StatusCode = HttpStatusCode.BadRequest;
            response.IsSuccess = false;
            response.ErrorMessage = e.Message;
        }
        
        return response;
    }

    public async Task<Response<List<CompanyResponseDto>>> SearchCompanies(CompanySearchQuery searchQuery)
    {
        var response = new Response<List<CompanyResponseDto>>();
        try
        {
        
            // get companies as a queryable object(include jobs + candidates)
            var companiesQueryable = applicationDbContext.Companies
                .AsQueryable();
            
            // make conditional checks based on searchQuery and query against the queryable object
            if (searchQuery.Size.HasValue)
            {
                companiesQueryable =
                    companiesQueryable.Where(company => company.Size == searchQuery.Size);
            }

            if (!string.IsNullOrEmpty(searchQuery.CompanyName))
            {
                companiesQueryable =
                    companiesQueryable.Where(company =>
                        company.Name.Contains(searchQuery.CompanyName));
            }
            
            // sort logic(can also add what to sort by 
            companiesQueryable = searchQuery.IsDescending
                ? companiesQueryable.OrderByDescending(c => c.Name)
                : companiesQueryable.OrderBy(c => c.Name);

            // Pagination logic
            // example: queryObject.PageNumber = 4,
            // queryObject.PageSize = 50
            // 4 - 1(starts at 0 so subtract 1, -1 -> prevents the PageNumber to be one ahead) = 3
            // 3 * 50 = 150, we skip the first 150 items and take(get) the next 50(PageSize) items
            var skipNumber = (searchQuery.PageNumber - 1) * searchQuery.PageSize;
            // skip the first X number of items and Take(get) Y number of items
            var companyList = await companiesQueryable.Skip(skipNumber).Take(searchQuery.PageSize).ToListAsync();


            response.Data = companyList.Select(mapper.Map<CompanyResponseDto>).ToList();
            response.StatusCode = HttpStatusCode.OK;
            

        }
        catch (Exception e)
        {
            response.IsSuccess = false;
            response.ErrorMessage = e.Message;
        }

        return response;
    }

    public async Task<Response<CompanyResponseDto>> GetCompanyById(long id)
    {
        var response = new Response<CompanyResponseDto>();
        try
        {
            var matchedCompany = await applicationDbContext.Companies.FirstOrDefaultAsync(
                company => company.Id == id);

            if (matchedCompany is null)
            {
                throw new Exception("There is no company with the ID provided");
            }
            response.Data = mapper.Map<CompanyResponseDto>(matchedCompany);
            response.StatusCode = HttpStatusCode.OK;
        }
        catch (Exception e)
        {
            response.StatusCode = HttpStatusCode.NoContent;
            response.IsSuccess = false;
            response.ErrorMessage = e.Message;
        }

        return response;
    }

    public async Task<BaseResponse> DeleteCompanyById(long id)
    {
        var response = new BaseResponse();
        try
        {
            var matchedCompany = await applicationDbContext.Companies.FindAsync(id);
            if (matchedCompany is null)
            {
                throw new Exception("Company with the provided id does not exist");
            }

            applicationDbContext.Companies.Remove(matchedCompany);
            await applicationDbContext.SaveChangesAsync();
            response.Message = $"Successfully deleted the company with the following id:{id}";
        }
        catch (Exception e)
        {
            response.IsSuccess = false;
            response.Message = e.Message;
        }
        
        return response;
    }
}