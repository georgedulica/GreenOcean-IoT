namespace GreenOcean.Business.Settings;

public class EmailPathSettings
{
    public string RegistrationEmailPath { get; set; }

    public string PasswordResetEmailPath { get; set; }
    
    public string TemperatureWarningEmailPath { get; set; }

    public string HumidityWarningEmailPath { get; set; }

    public string LightLevelWarningEmailPath { get; set; }

    public string SoilMoistureWarningEmailPath { get; set; }
}
