using GreenOcean.Data.Interfaces;
using GreenOcean.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GreenOcean.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext _dataContext;

    public UserRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Guid> GetUserByUsername(string username)
    {
        try
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => string.Equals(u.Username, username));
            if (user == null)
            {
                return Guid.Empty;
            }

            var userId = user.Id;
            return userId;
        }
        catch (Exception ex)
        {
            throw new Exception($"The user cannot be returned {ex}");
        }
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        try
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => string.Equals(u.Email, email));
            return user;
        }
        catch (Exception ex)
        {
            throw new Exception($"The user cannot be returned {ex}");
        }
    }

    public async Task<bool> AddUser(User user)
    {
        try
        {
            var email = user.Email;
            var existingEmail = await CheckEmail(email);
            if (existingEmail == true)
            {
                return false;
            }

            await _dataContext.Users.AddAsync(user);
            await _dataContext.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"The user cannot be saved {ex}");
        }
    }

    public async Task<bool> UpdateUser(Guid id, string username, byte[] password, byte[] salt)
    {
        try
        {
            var checkingUsername = await CheckUsername(username);
            if (checkingUsername == true)
            {
                return false;
            }

            var user = await GetUserById(id);
            if (user == null)
            {
                return false;
            }

            user.Username = username;
            user.Password = password;
            user.Salt = salt;

            await _dataContext.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"The user cannot be updated {ex}");
        }
    }

    public async Task<bool> UpdateUser(Guid id, byte[] password, byte[] salt)
    {
        try
        {
            var user = await GetUserById(id);
            if (user == null)
            {
                return false;
            }

            user.Password = password;
            user.Salt = salt;

            await _dataContext.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"The user cannot be updated {ex}");
        }
    }

    public async Task<bool> DeleteUser(User user)
    {
        try
        {
            _dataContext.Users.Remove(user);
            await _dataContext.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"The user cannot be deleted {ex}");
        }
    }

    private async Task<bool> CheckEmail(string email)
    {
        var existingUser = await _dataContext.Users.AnyAsync(u => string.Equals(u.Email, email));
        return existingUser;
    }

    private async Task<bool> CheckUsername(string username)
    {
        var existingUser = await _dataContext.Users.AnyAsync(u => string.Equals(u.Username, username));
        return existingUser;
    }

    private async Task<User?> GetUserById(Guid id)
    {
        try
        {
            var existingUser = await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            return existingUser;
        }
        catch (Exception ex)
        {
            throw new Exception($"The user cannot be found {ex}");
        }
    }
}