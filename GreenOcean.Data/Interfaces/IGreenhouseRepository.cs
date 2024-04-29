using GreenOcean.Data.Entities;

namespace GreenOcean.Data.Interfaces;

public interface IGreenhouseRepository
{
    public Task<IEnumerable<Greenhouse>?> GetGreenhouses(string username);

    public Task<Greenhouse?> GetGreenhouse(Guid id);

    public Task<bool> AddGreenhouse(string username, Greenhouse greenhouseDTO);

    public Task<bool> EditGreenhouse(Greenhouse greenhouseToEdit, Greenhouse greenhouse);

    public Task<bool> DeleteGreenhouse(Greenhouse greenhouse);
}