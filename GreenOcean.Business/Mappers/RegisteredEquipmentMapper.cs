using AutoMapper;
using GreenOcean.Business.DTOs;
using GreenOcean.Data.Entities;

namespace GreenOcean.Business.Mappers;

public class RegisteredEquipmentMapper : Profile
{
    public RegisteredEquipmentMapper()
    {
        CreateMap<RegisteredEquipmentDTO, RegisteredEquipment>();
    }
}