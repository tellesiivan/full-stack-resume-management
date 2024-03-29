using AutoMapper;
using backend.Core.Dtos.Candidate;
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
        CreateMap<CompanyResponseDto, Company>().ReverseMap();
        // job
        CreateMap<JobCreateDto, Job>().ReverseMap();
        CreateMap<JobResponseDto, Job>()
            .ReverseMap();
        // candidate
        CreateMap<CandidatesResponseDto, Candidate>()
            .ReverseMap();
            // we wan to make our destination member CandidatesResponseDto JobTitle property equal to the source Candidate candidate.Job!.Title
            
        CreateMap<Candidate, CandidateCreateDto>().ReverseMap();
    }
}