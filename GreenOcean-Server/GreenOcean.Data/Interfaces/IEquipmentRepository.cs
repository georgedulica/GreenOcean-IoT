using GreenOcean.Data.Entities;

namespace GreenOcean.Data.Interfaces;

public interface IEquipmentRepository
{
    public Task<Equipment?> GetEquipment(Guid id);

    public Task<IEnumerable<Equipment>> GetEquipments(Guid id);

    public Task<Equipment?> GetEquipmentByCode(Guid id);


    public Task<bool> AddEquipment(Equipment equipment);

    public Task<bool> EditEquipment(Equipment equipmentToEdit, Equipment equipment);

    public Task<bool> DeleteEquipment(Equipment equipment);
}