using ApplicationAPI.DTOs;
using AutoMapper;
using DomainModule.Entities;

namespace ApplicationAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserRequestDto>();
            CreateMap<InsurancePolicy, InsurancePolicyRequestDto>();
        }
    }
}