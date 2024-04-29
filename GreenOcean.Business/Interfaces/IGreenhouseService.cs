using GreenOcean.Business.DTOs;

namespace GreenOcean.Business.Interfaces;

public interface IGreenhouseService
{
    public Task<IEnumerable<GreenhouseDTO>?> GetGreenhouses(string username);

    public Task<GreenhouseDTO?> GetGreenhouse(Guid id);

    public Task<bool> AddGreenhouse(string username, GreenhouseDTO greenhouseDTO);

    public Task<bool> EditGreenhouse(Guid id, GreenhouseDTO greenhouseDTO);

    public Task<bool> DeleteGreenhouse(Guid id);
}