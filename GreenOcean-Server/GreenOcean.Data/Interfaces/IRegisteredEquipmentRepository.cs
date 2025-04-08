using GreenOcean.Data.Entities;

namespace GreenOcean.Data.Interfaces;

public interface IRegisteredEquipmentRepository
{
    public Task<RegisteredEquipment?> GetRegisteredEquipment(Guid code);

    public Task<bool> AddRegisteredEquipment(RegisteredEquipment registeredEquipment);
}