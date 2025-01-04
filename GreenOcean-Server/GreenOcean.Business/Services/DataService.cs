using AutoMapper;
using GreenOcean.Business.DTOs;
using GreenOcean.Business.Interfaces;
using GreenOcean.Data.Entities;
using GreenOcean.Data.Interfaces;
using SensorData = GreenOcean.Data.Data;

namespace GreenOcean.Business.Services;

public class DataService : IDataService
{
    private readonly IDataRepository _dataRepository;
    private readonly IEquipmentRepository _equipmentRepository;
    private readonly IMapper _mapper;

    public DataService(IDataRepository dataRepository, IEquipmentRepository equipmentRepository, IMapper mapper)
    {
        _dataRepository = dataRepository;
        _equipmentRepository = equipmentRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DataDTO>> GetDataByTimestamp(string timestamp)
    {
        try
        {
            var data = await _dataRepository.GetDataByTimestamp(timestamp);
            var dataDTO = _mapper.Map<IEnumerable<SensorData>, IEnumerable<DataDTO>>(data);
            return dataDTO;
        }
        catch (Exception exception)
        {
            var message = exception.Message;
            throw new Exception(message);
        }
    }

    public async Task<IEnumerable<DataDTO>> GetData(Guid id, string timestamp)
    {
        try
        {
            var data = await _dataRepository.GetData(id, timestamp);
            var dataDTOs = _mapper.Map<IEnumerable<SensorData>, IEnumerable<DataDTO>>(data);

            var equipment = await _equipmentRepository.GetEquipmentByCode(id);
            if (equipment == null)
            {
                return new List<DataDTO>();
            }

            var equipmentDTO = _mapper.Map<Equipment, EquipmentDTO>(equipment);

            foreach (var dataDTO in dataDTOs)
            {
                dataDTO.Equipment = equipmentDTO;
            }

            return dataDTOs;
        }
        catch (Exception exception)
        {
            var message = exception.Message;
            throw new Exception(message);
        }
    }

    public async Task<bool> DeleteData(Guid id, string timestamp)
    {
        try
        {
            var response = await _dataRepository.DeleteData(id, timestamp);
            return response;
        }
        catch (Exception exception)
        {
            var message = exception.Message;
            throw new Exception(message);
        }
    }
}