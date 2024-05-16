using AutoMapper;
using GreenOcean.Business.DTOs;
using SensorData = GreenOcean.Data.Data;

namespace GreenOcean.Business.Mappers;

public class DataMapper : Profile 
{
    public DataMapper()
    {
        CreateMap<SensorData, DataDTO>()
            .ForMember(dest => dest.EquipmentId, opt => opt.MapFrom(src => src.SystemId));
    }
}