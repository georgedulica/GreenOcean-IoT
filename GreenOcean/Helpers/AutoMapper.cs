using AutoMapper;
using GreenOcean.Business.DTOs;
using GreenOcean.Data.Entities;

namespace GreenOcean.Helpers;

public class AutoMapper : Profile
{

    public AutoMapper()
    {
        CreateMap<Greenhouse, GreenhouseDTO>();
        CreateMap<GreenhouseDTO, Greenhouse>();
        CreateMap<Plant, PlantDTO>();
        CreateMap<PlantDTO, Plant>();
        CreateMap<Equipment, EquipmentDTO>();
        CreateMap<EquipmentDTO, Equipment>();
        CreateMap<RegisteredEquipmentDTO, RegisteredEquipmentDTO>();
        CreateMap<SensorData, DataDTO>();
        CreateMap<Process, ProcessDTO>();
        CreateMap<ProcessDTO, Process>();
    }
}