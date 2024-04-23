using GreenOcean.Data.Entities;

namespace GreenOcean.Data.Interfaces;

public interface IRegisteredEquipmentRepository
{
    public Task<bool> AddRegisteredEquipment(RegisteredEquipment registeredEquipment);
}