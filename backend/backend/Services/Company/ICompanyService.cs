using backend.Core.Dtos.Company;
using backend.Core.Models;

namespace backend.Services.Company;

public interface ICompanyService
{
    Task<Response<string>> CreateCompany(CompanyCreationDto companyCreationDto);
    
    Task<Response<List<Core.Entities.Company>>> SearchCompanies(CompanySearchQuery searchQuery);
    Task<Response<Core.Entities.Company>> GetCompanyById(long id);
}