namespace GreenOcean.Interfaces;

public interface ISensorDataVerification
{
    Task CheckSensorData(DateTime time);
}
