namespace GreenOcean.Business.Interfaces;

public interface ITokenService
{
    public string CreateLoginToken(string name, string role);

    public string CreateConfirmationCodeToken(string name);
}