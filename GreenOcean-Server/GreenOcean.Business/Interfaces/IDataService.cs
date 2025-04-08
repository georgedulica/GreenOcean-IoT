using GreenOcean.Business.DTOs;

namespace GreenOcean.Business.Interfaces;

public interface IDataService
{
    public Task<IEnumerable<DataDTO>> GetData(Guid id, string timestamp);

    public Task<IEnumerable<DataDTO>> GetDataByTimestamp(string timestamp);

    public Task<bool> DeleteData(Guid id, string timestamp);
}