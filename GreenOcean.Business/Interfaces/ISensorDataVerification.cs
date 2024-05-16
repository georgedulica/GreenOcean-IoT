namespace GreenOcean.Business.Interfaces;

public interface ISensorDataVerification
{
    Task CheckSensorData(DateTime time);
}
