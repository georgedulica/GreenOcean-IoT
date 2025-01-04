using GreenOcean.Data.Entities;
using GreenOcean.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GreenOcean.Data.Repositories;

public class PlantRepository : IPlantRepository
{
    private readonly DataContext _dataContext;

    public PlantRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public Task<Plant?> GetPlant(Guid id)
    {
        try
        {
            var plant = _dataContext.Plants.FirstOrDefaultAsync(p => p.Id == id);
            return plant;
        }
        catch (Exception ex)
        {
            var message = $"The plant cannot be returned {ex.Message}";
            Console.WriteLine(message);
            throw new Exception(message);
        }
    }

    public async Task<IEnumerable<Plant>> GetPlants(Guid id)
    {
        try
        {
            var plants = await _dataContext.Plants.Where(p => p.GreenhouseId == id).ToListAsync();
            return plants;
        }
        catch (Exception ex)
        {
            var message = $"The plants cannot be returned {ex.Message}";
            Console.WriteLine(message);
            throw new Exception(message);
        }
    }

    public async Task<bool> AddPlant(Plant plant)
    {
        try
        {
            var checkingPlant = await ChecksPlant(plant);
            if (checkingPlant == true)
            {
                return false;
            }

            await _dataContext.Plants.AddAsync(plant);
            await _dataContext.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            var message = $"The plant cannot be added {ex.Message}";
            Console.WriteLine(message);
            throw new Exception(message);
        }
    }

    public async Task<bool> EditPlant(Plant plantToEdit, Plant plant)
    {
        try
        {
            var existingPlant = await ChecksPlant(plant);
            if (existingPlant == true)
            {
                return false;
            }

            plantToEdit.Name = plant.Name;
            plantToEdit.Height = plant.Height;
            plantToEdit.Type = plant.Type;
            plantToEdit.MositureLevel = plant.MositureLevel;
            plantToEdit.Humidity = plant.Humidity;
            plantToEdit.Soil = plant.Soil;
            plantToEdit.Temperature = plant.Temperature;
            plantToEdit.GreenhouseId = plant.GreenhouseId;

            await _dataContext.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            var message = $"The plant cannot be added {ex.Message}";
            Console.WriteLine(message);
            throw new Exception(message);
        }
    }

    public async Task<bool> DeletePlant(Plant plant)
    {
        try
        {
            _dataContext.Plants.Remove(plant);
            await _dataContext.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            var message = $"The plant cannot be removed {ex.Message}";
            Console.WriteLine(message);
            throw new Exception(message);
        }
    }

    private async Task<bool> ChecksPlant(Plant plant)
    {
        var existingPlant = await _dataContext.Plants.AnyAsync(p => string.Equals(p.Name, plant.Name) && p.Height == plant.Height &&
            string.Equals(p.Type, plant.Type) && p.MositureLevel == plant.MositureLevel && p.Humidity == plant.Humidity &&
            p.Temperature == plant.Temperature && string.Equals(p.Soil, plant.Soil) && p.GreenhouseId == plant.GreenhouseId);

        return existingPlant;
    }
}