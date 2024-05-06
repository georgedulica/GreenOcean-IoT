using AutoMapper;
using GreenOcean.Business.DTOs;
using GreenOcean.Business.Interfaces;
using GreenOcean.Data.Entities;
using GreenOcean.Data.Interfaces;

namespace GreenOcean.Business.Services;

public class EquipmentService : IEquipmentService
{
    private readonly IEquipmentRepository _equipmentRepository;
    private readonly IMapper _mapper;

    public EquipmentService(IEquipmentRepository equipmentRepository, IMapper mapper)
    {
        _equipmentRepository = equipmentRepository;
        _mapper = mapper;
    }

    public async Task<EquipmentDTO?> GetEquipment(Guid id)
    {
        try
        {
            var equipment = await _equipmentRepository.GetEquipment(id);
            if (equipment == null)
            {
                return null;
            }

            var equipmentDTO = _mapper.Map<Equipment, EquipmentDTO>(equipment);
            return equipmentDTO;
        }
        catch (Exception exception)
        {
            var message = exception.Message;
            Console.WriteLine(message);
            throw new Exception(message);
        }
    }

    public async Task<IEnumerable<EquipmentDTO>> GetEquipments(Guid id)
    {
        try
        {
            var equipment = await _equipmentRepository.GetEquipments(id);
            var equipmentDTOs = _mapper.Map<IEnumerable<Equipment>, IEnumerable<EquipmentDTO>>(equipment);
            return equipmentDTOs;
        }
        catch (Exception exception)
        {
            var message = exception.Message;
            Console.WriteLine(message);
            throw new Exception(message);
        }
    }

    public async Task<bool> AddEquipment(EquipmentDTO equipmentDTO)
    {
        try
        {
            var registeredEquipmentId = equipmentDTO.Code.ToString();
            var equipment = _mapper.Map<EquipmentDTO, Equipment>(equipmentDTO);

            var response = await _equipmentRepository.AddEquipment(registeredEquipmentId, equipment);
            return response;
        }
        catch (Exception exception)
        {
            var message = exception.Message;
            Console.WriteLine(message);
            throw new Exception(message);
        }
    }

    public async Task<bool> EditEquipment(Guid id, EquipmentDTO equipmentDTO)
    {
        try 
        {
            var equipmentToEdit = await _equipmentRepository.GetEquipment(id);
            if (equipmentToEdit == null)
            {
                return false;
            }

            var equipment = _mapper.Map<EquipmentDTO, Equipment>(equipmentDTO);
            var response = await _equipmentRepository.EditEquipment(equipmentToEdit, equipment);
            return response;
        }
        catch (Exception exception)
        {
            var message = exception.Message;
            Console.WriteLine(message);
            throw new Exception(message);
        }
    }

    public async Task<bool> DeleteEquipment(Guid id)
    {
        try
        {
            var equipment = await _equipmentRepository.GetEquipment(id);
            if (equipment == null)
            {
                return false;
            }

            var response = await _equipmentRepository.DeleteEquipment(equipment);
            return response;
        }
        catch (Exception exception)
        {
            var message = exception.Message;
            Console.WriteLine(message);
            throw new Exception(message);
        }
    }
}