using GreenOcean.Data.Entities;

namespace GreenOcean.Data.Interfaces;

public interface IPlantRepository
{
    public Task<Plant?> GetPlant(Guid id);

    public Task<IEnumerable<Plant>> GetPlants(Guid id);

    public Task<bool> AddPlant(Plant plant);

    public Task<bool> EditPlant(Plant plantToEdit, Plant plant);

    public Task<bool> DeletePlant(Plant plant);
}