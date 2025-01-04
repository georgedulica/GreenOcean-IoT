namespace GreenOcean.Data.Interfaces;

public interface ILoginRepository
{
    public Task<string?> ExistsUser(string username, string password);
}