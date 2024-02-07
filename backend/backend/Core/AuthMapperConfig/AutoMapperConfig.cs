using AutoMapper;
using backend.Core.Dtos.Company;
using backend.Core.Entities;

namespace backend.Core.AuthMapperConfig;

public class AutoMapperConfig: Profile
{
    public AutoMapperConfig()
    {
        // company
        // from CompanyCreationDto to Company
        CreateMap<CompanyCreationDto, Company>().ReverseMap();
        // job

        // candidate
    }
}