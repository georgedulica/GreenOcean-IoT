using AutoMapper;
using GreenOcean.DTOs;
using GreenOcean.Entities;
using SystemIoT = GreenOcean.Entities.System;

namespace GreenOcean.Helpers;

public class AutoMapper : Profile
{

    public AutoMapper()
    {
        CreateMap<Greenhouse, GreenhouseDTO>();
        CreateMap<GreenhouseDTO, Greenhouse>();
        CreateMap<Plant, PlantDTO>();
        CreateMap<PlantDTO, Plant>();
        CreateMap<SystemIoT, SystemDTO>();
        CreateMap<SystemDTO, SystemIoT>();
    }
}