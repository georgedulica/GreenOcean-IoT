using AutoMapper;
using GreenOcean.Business.DTOs;
using GreenOcean.Data.Entities;

namespace GreenOcean.Business.Mappers;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<User, UserDTO>();
        CreateMap<UserDTO, User>();
    }
}