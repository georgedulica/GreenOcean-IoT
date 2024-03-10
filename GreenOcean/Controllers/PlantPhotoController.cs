using GreenOcean.Data;
using GreenOcean.Interfaces;
using GreenOcean.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GreenOcean.Controllers;

[ApiController]
[Authorize]
[Route("plantphoto")]
public class PlantPhotoController : ControllerBase
{
    private readonly DataContext dataContext;
    private readonly IPhotoService photoService;
    private readonly IOptions<BasicPhotoSettings> config;

    public PlantPhotoController(DataContext dataContext, IPhotoService photoService, IOptions<BasicPhotoSettings> config)
    {
        this.dataContext = dataContext;
        this.photoService = photoService;
        this.config = config;
    }

    [HttpPost("changephtoto/{id}")]
    public async Task<IActionResult> ChangePhoto(IFormFile file, string id)
    {
        if (!Guid.TryParse(id, out Guid plantId))
        {
            return BadRequest("Invalid id format");
        }

        var plant = await dataContext.Plants.FirstOrDefaultAsync(p => p.Id == plantId);
        if (plant == null)
        {
            return BadRequest("Invalid id");
        }

        var deletingResult = await photoService.DeletePhoto(plant.PhotoId);
        if (deletingResult.Error != null)
        {
            return BadRequest("The plant cannot be uploaded");
        }

        var result = await photoService.AddPhoto(file);
        if (result.Error != null)
        {
            plant.PhotoURL = config.Value.URL;
            plant.PhotoId = config.Value.PublicId;
            await dataContext.SaveChangesAsync();

            return BadRequest("The photo cannot be uploaded");
        }

        try
        {
            plant.PhotoURL = result.SecureUrl.AbsoluteUri;
            plant.PhotoId = result.PublicId;
            await dataContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            plant.PhotoURL = config.Value.URL;
            plant.PhotoId = config.Value.PublicId;
            await dataContext.SaveChangesAsync();

            Console.WriteLine(ex);
            return BadRequest("The photo cannot be deleted");
        }

        return Ok();
    }

    [HttpDelete("deletephtoto/{id}")]
    public async Task<IActionResult> DeletePhoto(string id)
    {
        if (!Guid.TryParse(id, out Guid plantId))
        {
            return BadRequest("Invalid id format");
        }

        var plant = await dataContext.Plants.FirstOrDefaultAsync(p => p.Id == plantId);
        if (plant == null)
        {
            return BadRequest();
        }

        if (string.Equals(plant.PhotoURL, config.Value.URL) && string.Equals(plant.PhotoId, config.Value.PublicId))
        {
            return BadRequest("You cannot delete default picture");
        }

        var deletingResult = await photoService.DeletePhoto(plant.PhotoId);
        if (deletingResult.Error != null)
        {
            return BadRequest("The plant cannot be edited");
        }

        plant.PhotoURL = config.Value.URL;
        plant.PhotoId = config.Value.PublicId;
        await dataContext.SaveChangesAsync();

        return Ok();
    }
}