using Amazon.DynamoDBv2.DataModel;
using GreenOcean.Data.Interfaces;

namespace GreenOcean.Data.Repositories;

public class DataRepository : IDataRepository
{
    private readonly IDynamoDBContext _dynamoDBContext;

    public DataRepository(IDynamoDBContext dynamoDBContext)
    {
        _dynamoDBContext = dynamoDBContext;
    }

    public async Task<IEnumerable<Data>> GetDataByTimestamp(string timestamp)
    {
        try
        {
            var conditions = new List<ScanCondition>();
            var allData = await _dynamoDBContext.ScanAsync<Data>(conditions).GetRemainingAsync();
            var filteredData = FilterData(timestamp, allData).ToList();
            return filteredData;
        }
        catch (Exception exception)
        {
            var message = exception.Message;
            throw new Exception(message);
        }
    }

    public async Task<IEnumerable<Data>> GetData(Guid id, string timestamp)
    {
        try
        {
            var stringId = id.ToString().ToUpper();
            var data = await _dynamoDBContext.QueryAsync<Data>(stringId).GetRemainingAsync();

            var filteredData = FilterData(timestamp, data).ToList();
            return filteredData;
        }
        catch (Exception exception)
        {
            var message = exception.Message;
            throw new Exception(message);
        }
    }

    private IEnumerable<Data> FilterData(string timestamp, IEnumerable<Data> data)
    {
        var filteredData = new List<Data>();
        foreach (var item in data)
        {
            var dateTime = DateTime.ParseExact(item.Timestamp, "yyyy-MM-dd HH:mm:ss.ffffff", null);
            var formattedDate = dateTime.ToString("yyyy-MM-dd");

            if (string.Equals(formattedDate, timestamp))
            {
                filteredData.Add(item);
            }
        }

        return filteredData;
    }

    public async Task<bool> DeleteData(Guid id, string timestamp)
    {
        try
        {
            var code = id.ToString().ToUpper();

            var itemToDelete = new Data
            {
                SystemId = code,
                Timestamp = timestamp
            };

            await _dynamoDBContext.DeleteAsync(itemToDelete);

            return true;
        }
        catch (Exception exception)
        {
            var message = exception.Message;
            throw new Exception(message);
        }
    }
}