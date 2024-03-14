using AutoMapper;
using GreenOcean.DTOs;
using GreenOcean.Entities;
using RegisteredSystem = GreenOcean.Entities.System;

namespace GreenOcean.Helpers;

public class AutoMapper : Profile
{

    public AutoMapper()
    {
        CreateMap<Greenhouse, GreenhouseDTO>();
        CreateMap<GreenhouseDTO, Greenhouse>();
        CreateMap<Plant, PlantDTO>();
        CreateMap<PlantDTO, Plant>();
        CreateMap<IoTSystem, IoTSystemDTO>();
        CreateMap<IoTSystemDTO, IoTSystem>();
        CreateMap<SystemDTO, RegisteredSystem>();
    }
}