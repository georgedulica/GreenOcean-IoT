namespace GreenOcean.Business.Settings;

public class EmailSubjectSettings
{
    public string RegistrationEmailSubject { get; set; }

    public string PasswordResetEmailSubject { get; set; }

    public string TemperatureWarningEmailSubject { get; set; }

    public string HumidityWarningEmailSubject { get; set; }

    public string LightLevelWarningEmailSubject { get; set; }

    public string SoilMoistureWarningEmailSubject { get; set; }
}