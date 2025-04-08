using GreenOcean.Business.Interfaces;
using GreenOcean.Business.Settings;
using GreenOcean.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GreenOcean.Business.Services;

public class SensorDataVerification : ISensorDataVerification
{
    private readonly DataContext _dataContext;
    private readonly IEmailService _emailService;
    private readonly IDataService _dataService;
    private readonly IOptions<EmailPathSettings> _emailPathSettings;
    private readonly IOptions<EmailSubjectSettings> _emailSubjectSettings;

    public SensorDataVerification(DataContext dataContext, IDataService dataService, IEmailService emailService,
        IOptions<EmailPathSettings> emailPathSettings, IOptions<EmailSubjectSettings> emailSubjectSettings)
    {
        _dataContext = dataContext;
        _emailService = emailService;
        _emailPathSettings = emailPathSettings;
        _emailSubjectSettings = emailSubjectSettings;
        _dataService = dataService;
    }

    public async Task CheckSensorData(DateTime currentTime)
    {
        var today = DateTime.Now;

        string todayString = today.ToString("yyyy.MM.dd");
        var sensorData = await _dataService.GetDataByTimestamp(todayString);
        var equipment = await _dataContext.Equipment.Include(e => e.RegisteredEquipment).ToListAsync();

        foreach (var data in sensorData)
        {
            var timestamp = DateTime.Parse(data.Timestamp);

            if (timestamp >= currentTime)
            {
                var associatedEquipment = equipment.FirstOrDefault(re => string.Equals(re.RegisteredEquipment.Code, data.EquipmentId));
                if (associatedEquipment == null)
                {
                    continue;
                }

                var temperature = data.Temperature;
                var maxTemperature = associatedEquipment.MaxTemperature;
                var minTemperature = associatedEquipment.MinTemperature;

                var humidity = data.Humidity;
                var maxHumidity = associatedEquipment.MaxHumidity;
                var minHumidity = associatedEquipment.MinHumidity;

                var lightLevel = data.LightLevel;
                var maxLightLevel = associatedEquipment.MaxLightLevel;
                var minLightLevel = associatedEquipment.MinLightLevel;

                var soilMoisture = data.SoilMoisture;

                if (temperature > maxTemperature || temperature < minTemperature)
                {
                    var query = (from e in equipment
                                 join g in _dataContext.Greenhouses on e.GreenhouseId equals g.Id
                                 join u in _dataContext.Users on g.UserId equals u.Id
                                 where string.Equals(e.RegisteredEquipment.Code, data.EquipmentId)
                                 select new
                                 {
                                     UserFirstName = u.FirstName,
                                     UserEmail = u.Email,
                                     GreenhouseName = g.Name
                                 }).FirstOrDefault();

                    string emailTemplate = System.IO.File.ReadAllText(_emailPathSettings.Value.TemperatureWarningEmailPath);
                    string emailBody = emailTemplate.Replace("{name}", query.UserFirstName)
                                                    .Replace("{greenhouse}", query.GreenhouseName)
                                                    .Replace("{temperature}", data.Temperature.ToString())
                                                    .Replace("{timestamp}", DateTime.Parse(data.Timestamp).ToString("HH:mm:ss dd:MM:yyyy"));

                    _emailService.SendEmail(query.UserEmail, emailBody, _emailSubjectSettings.Value.TemperatureWarningEmailSubject);
                }

                if (humidity > maxHumidity || humidity < minHumidity)
                {
                    var query = (from e in equipment
                                 join g in _dataContext.Greenhouses on e.GreenhouseId equals g.Id
                                 join u in _dataContext.Users on g.UserId equals u.Id
                                 where string.Equals(e.RegisteredEquipment.Code, data.EquipmentId)
                                 select new
                                 {
                                     UserFirstName = u.FirstName,
                                     UserEmail = u.Email,
                                     GreenhouseName = g.Name
                                 }).FirstOrDefault();

                    string emailTemplate = System.IO.File.ReadAllText(_emailPathSettings.Value.HumidityWarningEmailPath);
                    string emailBody = emailTemplate.Replace("{name}", query.UserFirstName)
                                                    .Replace("{greenhouse}", query.GreenhouseName)
                                                    .Replace("{humidity}", data.Humidity.ToString())
                                                    .Replace("{timestamp}", DateTime.Parse(data.Timestamp).ToString("HH:mm:ss dd:MM:yyyy"));

                    _emailService.SendEmail(query.UserEmail, emailBody, _emailSubjectSettings.Value.HumidityWarningEmailSubject);
                }

                if (lightLevel > maxLightLevel || lightLevel < minLightLevel)
                {
                    var query = (from e in equipment
                                 join g in _dataContext.Greenhouses on e.GreenhouseId equals g.Id
                                 join u in _dataContext.Users on g.UserId equals u.Id
                                 where string.Equals(e.RegisteredEquipment.Code, data.EquipmentId)
                                 select new
                                 {
                                     UserFirstName = u.FirstName,
                                     UserEmail = u.Email,
                                     GreenhouseName = g.Name
                                 }).FirstOrDefault();

                    string emailTemplate = System.IO.File.ReadAllText(_emailPathSettings.Value.LightLevelWarningEmailPath);
                    string emailBody = emailTemplate.Replace("{name}", query.UserFirstName)
                                                    .Replace("{greenhouse}", query.GreenhouseName)
                                                    .Replace("{lightLevel}", data.LightLevel.ToString())
                                                    .Replace("{timestamp}", DateTime.Parse(data.Timestamp).ToString("HH:mm:ss dd:MM:yyyy"));

                    _emailService.SendEmail(query.UserEmail, emailBody, _emailSubjectSettings.Value.LightLevelWarningEmailSubject);
                }

                if (string.Equals(soilMoisture, "dry", StringComparison.OrdinalIgnoreCase))
                {
                    var query = (from e in equipment
                                 join g in _dataContext.Greenhouses on e.GreenhouseId equals g.Id
                                 join u in _dataContext.Users on g.UserId equals u.Id
                                 where string.Equals(e.RegisteredEquipment.Code, data.EquipmentId)
                                 select new
                                 {
                                     UserFirstName = u.FirstName,
                                     UserEmail = u.Email,
                                     GreenhouseName = g.Name
                                 }).FirstOrDefault();

                    string emailTemplate = System.IO.File.ReadAllText(_emailPathSettings.Value.SoilMoistureWarningEmailPath);
                    string emailBody = emailTemplate.Replace("{name}", query.UserFirstName)
                                                    .Replace("{greenhouse}", query.GreenhouseName)
                                                    .Replace("{soilMoisture}", data.SoilMoisture.ToString())
                                                    .Replace("{timestamp}", DateTime.Parse(data.Timestamp).ToString("HH:mm:ss dd:MM:yyyy"));

                    _emailService.SendEmail(query.UserEmail, emailBody, _emailSubjectSettings.Value.SoilMoistureWarningEmailSubject);
                }
            }
        }
    }
}