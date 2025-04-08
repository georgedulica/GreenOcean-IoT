using AutoMapper;
using GreenOcean.Business.DTOs;
using GreenOcean.Data.Entities;

namespace GreenOcean.Business.Mappers;

public class EquipmentMapper : Profile
{
    public EquipmentMapper()
    {
        CreateMap<Equipment, EquipmentDTO>()
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.RegisteredEquipment.Code));
        CreateMap<EquipmentDTO, Equipment>();
    }
}