using AutoMapper;
using GreenOcean.Business.DTOs;
using GreenOcean.Business.Interfaces;
using GreenOcean.Data.Entities;
using GreenOcean.Data.Interfaces;

namespace GreenOcean.Business.Services;

public class RegisteredEquipmentService : IRegisteredEquipmentService
{
    private readonly IRegisteredEquipmentRepository _registeredEquipmentRepository;
    private readonly IMapper _mapper;

    public RegisteredEquipmentService(IRegisteredEquipmentRepository registeredEquipmentRepository, IMapper mapper)
    {
        _registeredEquipmentRepository = registeredEquipmentRepository;
        _mapper = mapper;
    }

    public async Task<bool> AddRegisteredEquipment(RegisteredEquipmentDTO registeredEquipmentDTO)
    {
        try
        {
            var registeredEquipment = _mapper.Map<RegisteredEquipmentDTO, RegisteredEquipment>(registeredEquipmentDTO);
            var existingRegisteredEquipment = await _registeredEquipmentRepository.AddRegisteredEquipment(registeredEquipment);
            return existingRegisteredEquipment;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new ArgumentException($"{ex}");
        }
    }
}