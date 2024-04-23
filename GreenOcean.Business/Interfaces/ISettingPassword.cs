namespace GreenOcean.Business.Interfaces;

public interface ISettingPassword
{
    public byte[] EncryptPassword(string password, out byte[] salt);
}