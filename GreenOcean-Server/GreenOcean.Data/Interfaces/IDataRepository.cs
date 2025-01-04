namespace GreenOcean.Data.Interfaces;

public interface IDataRepository
{
    public Task<IEnumerable<Data>> GetData(Guid id, string timestamp);

    public Task<IEnumerable<Data>> GetDataByTimestamp(string timestamp);

    public Task<bool> DeleteData(Guid id, string timestamp);
}