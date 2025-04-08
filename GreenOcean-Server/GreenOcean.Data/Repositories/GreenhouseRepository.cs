using GreenOcean.Data.Entities;
using GreenOcean.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GreenOcean.Data.Repositories;

public class GreenhouseRepository : IGreenhouseRepository
{
    private readonly DataContext _dataContext;
    private readonly IUserRepository _userRepository;

    public GreenhouseRepository(DataContext dataContext, IUserRepository userRepository)
    {
        _dataContext = dataContext;
        _userRepository = userRepository;
    }

    public async Task<Greenhouse?> GetGreenhouse(Guid id)
    {
        try
        {
            var greenhouse = await _dataContext.Greenhouses.FirstOrDefaultAsync(u => u.Id == id);
            return greenhouse;
        }
        catch (Exception ex)
        {
            var message = $"The greenhouse cannot be returned {ex.Message}";
            Console.WriteLine(message);
            throw new Exception(message);
        }
    }

    public async Task<IEnumerable<Greenhouse>?> GetGreenhouses(string username)
    {
        try
        {
            var userId = await _userRepository.GetUserByUsername(username);
            if (userId == Guid.Empty)
            {
                return null;
            }

            var greenhouses = await _dataContext.Greenhouses.Where(g => string.Equals(g.UserId, userId)).ToListAsync();
            return greenhouses;
        }
        catch (Exception ex)
        {
            var message = $"The greenhouses cannot be returned {ex.Message}";
            Console.WriteLine(message);
            throw new Exception(message);
        }
    }

    public async Task<bool> AddGreenhouse(string username, Greenhouse greenhouse)
    {
        try
        {
            var userId = await _userRepository.GetUserByUsername(username);
            if (userId == Guid.Empty)
            {
                return false;
            }

            greenhouse.UserId = userId;

            var existingGreenhouse = await CheckGreenhouse(greenhouse);
            if (existingGreenhouse == true)
            {
                return false;
            }

            await _dataContext.Greenhouses.AddAsync(greenhouse);
            await _dataContext.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            var message = $"The greenhouses cannot be added {ex.Message}";
            Console.WriteLine(message);
            throw new Exception(message);
        }
    }

    public async Task<bool> EditGreenhouse(Greenhouse greenhouseToEdit, Greenhouse greenhouse)
    {
        try
        {
            greenhouse.UserId = greenhouseToEdit.UserId;
            var existingGreenhouse = await CheckGreenhouse(greenhouse);
            if (existingGreenhouse)
            {
                return false;
            }

            greenhouseToEdit.Name = greenhouse.Name;
            greenhouseToEdit.Street = greenhouse.Street;
            greenhouseToEdit.Number = greenhouse.Number;
            greenhouseToEdit.City = greenhouse.City;

            await _dataContext.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            var message = $"The greenhouse cannot be updated {ex.Message}";
            Console.WriteLine(message);
            throw new Exception(message);
        }
    }

    public async Task<bool> DeleteGreenhouse(Greenhouse greenhouse)
    {
        try
        {
            _dataContext.Greenhouses.Remove(greenhouse);
            await _dataContext.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            var message = $"The greenhouses= cannot be removed {ex.Message}";
            Console.WriteLine(message);
            throw new Exception(message);
        }
    }

    private async Task<bool> CheckGreenhouse(Greenhouse greenhouse)
    {
        var existingGreenhouse = await _dataContext.Greenhouses.AnyAsync(g => string.Equals(g.Name, greenhouse.Name) &&
            string.Equals(g.Street, greenhouse.Street) && g.Number == greenhouse.Number && string.Equals(g.City, greenhouse.City) 
            && g.UserId == greenhouse.UserId);
        return existingGreenhouse;
    }    
    
    private async Task<bool> CheckFirstGreenhouse(Greenhouse greenhouse)
    {
        var existingGreenhouse = await _dataContext.Greenhouses.AnyAsync(g => string.Equals(g.Name, greenhouse.Name) &&
            string.Equals(g.Street, greenhouse.Street) && g.Number == greenhouse.Number && string.Equals(g.City, greenhouse.City) 
            && g.UserId == greenhouse.UserId);
        return existingGreenhouse;
    }
}