using AutoMapper;
using GreenOcean.Business.DTOs;
using GreenOcean.Data.Entities;

namespace GreenOcean.Business.Mappers;

public class PlantMapper : Profile
{
    public PlantMapper()
    {
        CreateMap<Plant, PlantDTO>();
        CreateMap<PlantDTO, Plant>();
    }
}