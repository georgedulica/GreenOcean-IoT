using GreenOcean.Business.DTOs;

namespace GreenOcean.Business.Interfaces;

public interface IEquipmentService
{
    public Task<EquipmentDTO?> GetEquipment(Guid id);

    public Task<IEnumerable<EquipmentDTO>> GetEquipments(Guid id);

    public Task<bool> AddEquipment(EquipmentDTO equipmentDTO);

    public Task<bool> EditEquipment(Guid id, EquipmentDTO equipmentDTO);

    public Task<bool> DeleteEquipment(Guid id);
}