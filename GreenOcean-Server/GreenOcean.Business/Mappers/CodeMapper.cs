using AutoMapper;
using GreenOcean.Business.DTOs;
using GreenOcean.Data.Entities;

namespace GreenOcean.Business.Mappers;

public class CodeMapper : Profile
{

    public CodeMapper()
    {
        CreateMap<Code, CodeDTO>();
        CreateMap<CodeDTO, Code>();
    }
}