using AutoMapper;
using GreenOcean.DTOs;
using GreenOcean.Entities;

namespace GreenOcean.Helpers;

public class AutoMapper : Profile
{

    public AutoMapper()
    {
        CreateMap<Greenhouse, GreenhouseDTO>();
        CreateMap<Plant, PlantDTO>();
    }
}