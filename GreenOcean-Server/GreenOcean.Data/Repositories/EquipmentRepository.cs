using GreenOcean.Data.Entities;
using GreenOcean.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GreenOcean.Data.Repositories;

public class EquipmentRepository : IEquipmentRepository
{
    private readonly DataContext _dataContext;

    public EquipmentRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Equipment?> GetEquipment(Guid id)
    {
        try
        {
            var equipment = await _dataContext.Equipment.FirstOrDefaultAsync(e => e.Id == id);
            return equipment;
        }
        catch (Exception exception)
        {
            var message = $"The equipment cannot be returned {exception.Message}";
            Console.WriteLine(message);
            throw new Exception($"{exception}");
        }
    }

    public async Task<Equipment?> GetEquipmentByCode(Guid id)
    {
        try
        {
            var equipment = await _dataContext.Equipment.FirstOrDefaultAsync(e => e.RegisteredEquipment.Code == id.ToString());
            return equipment;
        }
        catch (Exception exception)
        {
            var message = $"The equipment cannot be returned {exception.Message}";
            Console.WriteLine(message);
            throw new Exception($"{exception}");
        }
    }

    public async Task<IEnumerable<Equipment>> GetEquipments(Guid id)
    {
        try
        {
            var equipment = await _dataContext.Equipment.Where(e => e.GreenhouseId == id).Include(e => e.RegisteredEquipment).ToListAsync();
            return equipment;
        }
        catch (Exception exception)
        {
            var message = $"The equipments cannot be returned {exception.Message}";
            Console.WriteLine(message);
            throw new Exception($"{exception}");
        }
    }

    public async Task<bool> AddEquipment(Equipment equipment)
    {
        var existingEquipment = await ChecksEquipment(equipment);
        if (existingEquipment == true)
        {
            return false;
        }

        try
        {
            await _dataContext.Equipment.AddAsync(equipment);
            await _dataContext.SaveChangesAsync();

            return true;
        }
        catch (Exception exception)
        {
            var message = $"The equipment cannot be added {exception.Message}";
            Console.WriteLine(message);
            throw new Exception($"{exception}");
        }
    }

    public async Task<bool> EditEquipment(Equipment equipmentToEdit, Equipment equipment)
    {
        try
        {
            var checkingEquipment = await ChecksEquipment(equipment);
            if (checkingEquipment == true)
            {
                return false;
            }

            equipmentToEdit.Name = equipment.Name;
            equipmentToEdit.MaxTemperature = equipment.MaxTemperature;
            equipmentToEdit.MinTemperature = equipment.MinTemperature;
            equipmentToEdit.MaxHumidity = equipment.MaxHumidity;
            equipmentToEdit.MinHumidity = equipment.MinHumidity;
            equipmentToEdit.MaxLightLevel = equipment.MaxLightLevel;
            equipmentToEdit.MinLightLevel = equipment.MinLightLevel;
            equipmentToEdit.Status = equipment.Status;
            equipmentToEdit.GreenhouseId = equipment.GreenhouseId;

            await _dataContext.SaveChangesAsync();
            return true;
        }
        catch (Exception exception)
        {
            var message = $"The equipment cannot be edited {exception.Message}";
            Console.WriteLine(message);
            throw new Exception($"{exception}");
        }
    }

    public async Task<bool> DeleteEquipment(Equipment equipment)
    {
        try
        {
            _dataContext.Equipment.Remove(equipment);
            await _dataContext.SaveChangesAsync();

            return true;
        }
        catch (Exception exception)
        {
            var message = $"The equipment cannot be deleted {exception.Message}";
            Console.WriteLine(message);
            throw new Exception($"{exception}");
        }
    }

    private async Task<bool> ChecksEquipment(Equipment equipment)
    {
        var existingEquipment = await _dataContext.Equipment.AnyAsync(e => string.Equals(e.Name, equipment.Name) && 
                e.GreenhouseId == equipment.GreenhouseId && e.MaxLightLevel == equipment.MaxLightLevel &&
                string.Equals(e.Status, equipment.Status) && e.MinLightLevel == equipment.MinLightLevel &&
                e.MaxTemperature == equipment.MaxTemperature && e.MinTemperature == equipment.MinTemperature &&
                e.MaxHumidity == equipment.MaxHumidity && e.MinHumidity == equipment.MinHumidity);
        return existingEquipment;
    }
}