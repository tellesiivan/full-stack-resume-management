using AutoMapper;
using backend.Core.Dtos.Company;
using backend.Core.Dtos.Jobs;
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
        CreateMap<JobCreateDto, Job>().ReverseMap();
        CreateMap<JobResponseDto, Job>()
            .ReverseMap()
            .ForMember(destinationMember => destinationMember.CompanyName, 
                memberOptions => memberOptions.MapFrom(
                job => job.Company!.Name));
        // candidate
    }
}