using AutoMapper;
using GreenOcean.Business.DTOs;
using GreenOcean.Data.Entities;

namespace GreenOcean.Business.Mappers;

public class GreenhouseMapper : Profile
{
    public GreenhouseMapper()
    {
        CreateMap<Greenhouse, GreenhouseDTO>();
        CreateMap<GreenhouseDTO, Greenhouse>();
    }
}