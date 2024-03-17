using AutoMapper;
using GreenOcean.DTOs;
using GreenOcean.Entities;
using RegisteredSystem = GreenOcean.Entities.System;
using DynamoDBData = GreenOcean.Data.Data;

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
        CreateMap<DynamoDBData, DataDTO>();
        CreateMap<SensorData, DataDTO>();
    }
}