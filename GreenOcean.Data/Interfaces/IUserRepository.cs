using GreenOcean.Data.Entities;

namespace GreenOcean.Data.Interfaces;

public interface IUserRepository
{
    public Task<bool> AddUser(User user);

    public Task<bool> UpdateUser(Guid id, string username, byte[] password, byte[] salt);

    public Task<bool> DeleteUser(User user);
}