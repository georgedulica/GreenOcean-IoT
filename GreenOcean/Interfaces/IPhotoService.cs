using CloudinaryDotNet.Actions;

namespace GreenOcean.Interfaces;

public interface IPhotoService
{
    Task<ImageUploadResult> AddPhoto(IFormFile file);

    Task<DeletionResult> DeletePhoto(string publicId);
}
