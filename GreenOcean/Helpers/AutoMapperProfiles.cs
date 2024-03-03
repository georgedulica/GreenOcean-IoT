using AutoMapper;
using GreenOcean.DTOs;
using GreenOcean.Entities;

namespace GreenOcean.Helpers;

public class AutoMapperProfiles : Profile
{

    public AutoMapperProfiles()
    {
        CreateMap<Greenhouse, GreenhouseDTO>();
    }
}