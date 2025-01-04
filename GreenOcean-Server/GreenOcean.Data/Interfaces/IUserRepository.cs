using GreenOcean.Data.Entities;

namespace GreenOcean.Data.Interfaces;

public interface IUserRepository
{
    public Task<Guid> GetUserByUsername(string username);

    public Task<User?> GetUserByEmail(string email);

    public Task<bool> AddUser(User user);

    public Task<bool> UpdateUser(Guid id, string username, byte[] password, byte[] salt);

    public Task<bool> UpdateUser(Guid id, byte[] password, byte[] salt);

    public Task<bool> DeleteUser(User user);
}