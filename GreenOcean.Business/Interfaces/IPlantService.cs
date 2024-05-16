using GreenOcean.Business.DTOs;

namespace GreenOcean.Business.Interfaces;

public interface IPlantService
{
    public Task<PlantDTO?> GetPlant(Guid id);

    public Task<IEnumerable<PlantDTO>> GetPlants(Guid id);

    public Task<bool> AddPlant(PlantDTO plantDTO);

    public Task<bool> EditPlant(Guid id, PlantDTO plantDTO);

    public Task<bool> DeletePlant(Guid id);
}