using GreenOcean.Business.DTOs;

namespace GreenOcean.Business.Interfaces;

public interface IRegisteredEquipmentService
{
    public Task<bool> AddRegisteredEquipment(RegisteredEquipmentDTO registeredEquipmentDTO);
}
