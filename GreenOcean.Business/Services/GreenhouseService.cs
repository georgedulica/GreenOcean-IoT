using AutoMapper;
using GreenOcean.Business.DTOs;
using GreenOcean.Business.Interfaces;
using GreenOcean.Data.Entities;
using GreenOcean.Data.Interfaces;

namespace GreenOcean.Business.Services;

public class GreenhouseService : IGreenhouseService
{
    private readonly IGreenhouseRepository _greenhouseRepository;
    private readonly IMapper _mapper;

    public GreenhouseService(IGreenhouseRepository greenhouseRepository, IMapper mapper)
    {
        _greenhouseRepository = greenhouseRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GreenhouseDTO>?> GetGreenhouses(string username)
    {
        try
        {
            var greenhouses = await _greenhouseRepository.GetGreenhouses(username);
            if (greenhouses == null)
            {
                return null;
            }

            var greenhousesDTO = _mapper.Map<IEnumerable<Greenhouse>, IEnumerable<GreenhouseDTO>>(greenhouses);
            return greenhousesDTO;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw new Exception(ex.Message);
        }
    }

    public async Task<GreenhouseDTO?> GetGreenhouse(Guid id)
    {
        try
        {
            var greenhouse = await _greenhouseRepository.GetGreenhouse(id);
            if (greenhouse == null)
            {
                return null;
            }

            var greenhouseDTO = _mapper.Map<Greenhouse, GreenhouseDTO>(greenhouse);
            return greenhouseDTO;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> AddGreenhouse(string username, GreenhouseDTO greenhouseDTO)
    {
        try
        {
            var greenhouse = _mapper.Map<GreenhouseDTO, Greenhouse>(greenhouseDTO);
            
            var response = await _greenhouseRepository.AddGreenhouse(username, greenhouse);
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> EditGreenhouse(Guid id, GreenhouseDTO greenhouseDTO)
    {
        try
        {
            var greenhouse = _mapper.Map<GreenhouseDTO, Greenhouse>(greenhouseDTO);

            var greenhouseToEdit = await _greenhouseRepository.GetGreenhouse(id);
            if (greenhouseToEdit == null)
            {
                return false;
            }

            var response = await _greenhouseRepository.EditGreenhouse(greenhouseToEdit, greenhouse);
            return response;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> DeleteGreenhouse(Guid id)
    {
        try
        {
            var greenhouse = await _greenhouseRepository.GetGreenhouse(id);
            if (greenhouse == null)
            {
                return false;
            }

            var response = await _greenhouseRepository.DeleteGreenhouse(greenhouse);
            return response;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}