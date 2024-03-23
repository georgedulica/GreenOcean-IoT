using GreenOcean.Data;
using GreenOcean.Entities;
using GreenOcean.Interfaces;
using GreenOcean.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GreenOcean.Services;

public class SensorDataVerification : ISensorDataVerification
{
    private readonly DataContext dataContext;
    private readonly IEmailService emailService;
    private readonly IOptions<EmailPathSettings> emailPathSettings;
    private readonly IOptions<EmailSubjectSettings> emailSubjectSettings;

    public SensorDataVerification(DataContext dataContext, IEmailService emailService, IOptions<EmailPathSettings> emailPathSettings,
        IOptions<EmailSubjectSettings> emailSubjectSettings)
    {
        this.dataContext = dataContext;
        this.emailService = emailService;
        this.emailPathSettings = emailPathSettings;
        this.emailSubjectSettings = emailSubjectSettings;
    }

    public async Task CheckSensorData(DateTime currentTime)
    {
        var sensorData = await dataContext.SensorData.Include(d => d.IoTSystem).ToListAsync();

        foreach (var data in sensorData)
        {
            var timestamp = DateTime.Parse(data.Timestamp);
            if (timestamp >= currentTime)
            {
                var temperature = data.Temperature;
                var maxTemperature = data.IoTSystem.MaxTemperature;
                var minTemperature = data.IoTSystem.MinTemperature;

                var humidity = data.Humidity;
                var maxHumidity = data.IoTSystem.MaxHumidity;
                var minHumidity = data.IoTSystem.MinHumidity;

                var lightLevel = data.LightLevel;
                var maxLightLevel = data.IoTSystem.MaxLightLevel;
                var minLightLevel = data.IoTSystem.MinLightLevel;

                var soilMoisture = data.SoilMoisture;

                if (temperature > maxTemperature || temperature < minTemperature)
                {
                    var query = await (from d in dataContext.SensorData
                                       join s in dataContext.IoTSystems on d.IoTSystemId equals s.Id
                                       join g in dataContext.Greenhouses on s.GreenhouseId equals g.Id
                                       join u in dataContext.Users on g.UserId equals u.Id
                                       where d.Id == data.Id
                                       select new
                                       {
                                           UserFirstName = u.FirstName,
                                           UserEmail = u.Email,
                                           GreenhouseName = g.Name
                                       }).FirstOrDefaultAsync();

                    string emailTemplate = System.IO.File.ReadAllText(emailPathSettings.Value.TemperatureWarningEmailPath);
                    string emailBody = emailTemplate.Replace("{name}", query.UserFirstName)
                                                    .Replace("{greenhouse}", query.GreenhouseName)
                                                    .Replace("{temperature}", data.Temperature.ToString())
                                                    .Replace("{timestamp}", DateTime.Parse(data.Timestamp).ToString("HH:mm:ss dd:MM:yyyy"));

                    emailService.SendEmail(query.UserEmail, emailBody, emailSubjectSettings.Value.TemperatureWarningEmailSubject);
                }

                if (humidity > maxHumidity || humidity < minHumidity)
                {
                    var query = await (from d in dataContext.SensorData
                                       join s in dataContext.IoTSystems on d.IoTSystemId equals s.Id
                                       join g in dataContext.Greenhouses on s.GreenhouseId equals g.Id
                                       join u in dataContext.Users on g.UserId equals u.Id
                                       where d.Id == data.Id
                                       select new
                                       {
                                           UserFirstName = u.FirstName,
                                           UserEmail = u.Email,
                                           GreenhouseName = g.Name
                                       }).FirstOrDefaultAsync();

                    string emailTemplate = System.IO.File.ReadAllText(emailPathSettings.Value.HumidityWarningEmailPath);
                    string emailBody = emailTemplate.Replace("{name}", query.UserFirstName)
                                                    .Replace("{greenhouse}", query.GreenhouseName)
                                                    .Replace("{humidity}", data.Humidity.ToString())
                                                    .Replace("{timestamp}", DateTime.Parse(data.Timestamp).ToString("HH:mm:ss dd:MM:yyyy"));

                    emailService.SendEmail(query.UserEmail, emailBody, emailSubjectSettings.Value.HumidityWarningEmailSubject);
                }

                if (lightLevel > maxLightLevel || lightLevel < minLightLevel)
                {
                    var query = await (from d in dataContext.SensorData
                                       join s in dataContext.IoTSystems on d.IoTSystemId equals s.Id
                                       join g in dataContext.Greenhouses on s.GreenhouseId equals g.Id
                                       join u in dataContext.Users on g.UserId equals u.Id
                                       where d.Id == data.Id
                                       select new
                                       {
                                           UserFirstName = u.FirstName,
                                           UserEmail = u.Email,
                                           GreenhouseName = g.Name
                                       }).FirstOrDefaultAsync();

                    string emailTemplate = System.IO.File.ReadAllText(emailPathSettings.Value.LightLevelWarningEmailPath);
                    string emailBody = emailTemplate.Replace("{name}", query.UserFirstName)
                                                    .Replace("{greenhouse}", query.GreenhouseName)
                                                    .Replace("{lightLevel}", data.LightLevel.ToString())
                                                    .Replace("{timestamp}", DateTime.Parse(data.Timestamp).ToString("HH:mm:ss dd:MM:yyyy"));

                    emailService.SendEmail(query.UserEmail, emailBody, emailSubjectSettings.Value.LightLevelWarningEmailSubject);
                }

                if (string.Equals(soilMoisture, "dry", StringComparison.OrdinalIgnoreCase))
                {
                    var query = await (from d in dataContext.SensorData
                                       join s in dataContext.IoTSystems on d.IoTSystemId equals s.Id
                                       join g in dataContext.Greenhouses on s.GreenhouseId equals g.Id
                                       join u in dataContext.Users on g.UserId equals u.Id
                                       where d.Id == data.Id
                                       select new
                                       {
                                           UserFirstName = u.FirstName,
                                           UserEmail = u.Email,
                                           GreenhouseName = g.Name
                                       }).FirstOrDefaultAsync();

                    string emailTemplate = System.IO.File.ReadAllText(emailPathSettings.Value.SoilMoistureWarningEmailPath);
                    string emailBody = emailTemplate.Replace("{name}", query.UserFirstName)
                                                    .Replace("{greenhouse}", query.GreenhouseName)
                                                    .Replace("{soilMoisture}", data.SoilMoisture.ToString())
                                                    .Replace("{timestamp}", DateTime.Parse(data.Timestamp).ToString("HH:mm:ss dd:MM:yyyy"));

                   emailService.SendEmail(query.UserEmail, emailBody, emailSubjectSettings.Value.SoilMoistureWarningEmailSubject);
                }
            }
        }
    }
}