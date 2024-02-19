using backend.Core.Dtos.Company;
using backend.Core.Models;

namespace backend.Services.Company;

public interface ICompanyService
{
    Task<Response<string>> CreateCompany(CompanyCreationDto companyCreationDto);
    
    Task<Response<List<CompanyResponseDto>>> SearchCompanies(CompanySearchQuery searchQuery);
    Task<Response<CompanyResponseDto>> GetCompanyById(long id);
    Task<BaseResponse> DeleteCompanyById(long id);
}