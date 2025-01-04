using AutoMapper;
using GreenOcean.Business.DTOs;
using GreenOcean.Business.Interfaces;
using GreenOcean.Business.Settings;
using GreenOcean.Data.Entities;
using GreenOcean.Data.Interfaces;
using Microsoft.Extensions.Options;

namespace GreenOcean.Business.Services;

public class PlantService : IPlantService
{
    private readonly IPlantRepository _plantRepository;
    private readonly IPhotoService _photoService;
    private readonly IMapper _mapper;
    private readonly IOptions<BasicPhotoSettings> _basicPhotoSettings;

    public PlantService(IPlantRepository plantRepository, IMapper mapper,
        IPhotoService photoService, IOptions<BasicPhotoSettings> basicPhotoSettings)
    {
        _plantRepository = plantRepository;
        _photoService = photoService;     
        _mapper = mapper;
        _basicPhotoSettings = basicPhotoSettings;
    }

    public async Task<PlantDTO?> GetPlant(Guid id)
    {
        try
        {
            var plant = await _plantRepository.GetPlant(id);
            if (plant == null)
            {
                return null;
            }

            var plantDTO = _mapper.Map<Plant, PlantDTO>(plant);
            return plantDTO;
        }
        catch (Exception ex)
        {
            var message = ex.Message;
            Console.WriteLine(message);
            throw new Exception(message);
        }
    }

    public async Task<IEnumerable<PlantDTO>> GetPlants(Guid id)
    {
        try
        {
            var plants = await _plantRepository.GetPlants(id);
            
            var plantDTOs = _mapper.Map<IEnumerable<Plant>, IEnumerable<PlantDTO>>(plants);
            return plantDTOs;
        }
        catch (Exception ex)
        {
            var message = $"The plants cannot be returned {ex.Message}";
            Console.WriteLine(message);
            throw new Exception(message);
        }
    }

    public async Task<bool> AddPlant(PlantDTO plantDTO)
    {
        try
        {
            var plant = _mapper.Map<PlantDTO, Plant>(plantDTO);

            plant.PhotoId = _basicPhotoSettings.Value.PublicId;
            plant.PhotoURL = _basicPhotoSettings.Value.URL;

            var response = await _plantRepository.AddPlant(plant);
            return response;
        }
        catch (Exception ex)
        {
            var message = $"The plants cannot be added {ex.Message}";
            Console.WriteLine(message);
            throw new Exception(message);
        }
    }

    public async Task<bool> EditPlant(Guid id, PlantDTO plantDTO)
    {
        try
        {
            var plantToEdit = await _plantRepository.GetPlant(id);
            if (plantToEdit == null)
            {
                throw new Exception("The plant cannot be found");
            }

            var plant = _mapper.Map<PlantDTO, Plant>(plantDTO);

            var response = await _plantRepository.EditPlant(plantToEdit, plant);
            return response;
        }
        catch (Exception ex)
        {
            var message = $"The plant cannot be updated {ex.Message}";
            Console.WriteLine(message);
            throw new Exception(message);
        }
    }

    public async Task<bool> DeletePlant(Guid id)
    {
        try
        {
            var plant = await _plantRepository.GetPlant(id);
            if (plant == null)
            {
                return false;
            }

            if (!string.Equals(plant.PhotoURL, _basicPhotoSettings.Value.URL))
            {
                var deletingResult = await _photoService.DeletePhoto(plant.PhotoId);
                if (deletingResult.Error != null)
                {
                    return false;
                }
            }

            var response = await _plantRepository.DeletePlant(plant);
            return response;
        }
        catch (Exception ex)
        {
            var message = $"The plant cannot be removed {ex.Message}";
            Console.WriteLine(message);
            throw new Exception(message);
        }
    }
}