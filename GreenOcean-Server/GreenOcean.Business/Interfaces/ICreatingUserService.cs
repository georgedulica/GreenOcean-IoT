using GreenOcean.Business.DTOs;

namespace GreenOcean.Business.Interfaces;

public interface ICreatingUserService
{
    public Task<bool> CreateUser(UserDTO userDTO);
}