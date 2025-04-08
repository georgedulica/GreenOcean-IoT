using AutoMapper;
using GreenOcean.Business.DTOs;
using GreenOcean.Data.Entities;

namespace GreenOcean.Business.Mappers;

public class ProcessMapper : Profile
{
    public ProcessMapper()
    {
        CreateMap<Process, ProcessDTO>();
        CreateMap<ProcessDTO, Process>();
    }
}