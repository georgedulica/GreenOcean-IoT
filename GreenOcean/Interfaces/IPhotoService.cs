using CloudinaryDotNet.Actions;

namespace GreenOcean.Interfaces;

public interface IPhotoService
{
    Task<ImageUploadResult> AddPhotoAsync(IFormFile file);

    Task<DeletionResult> DeletePhotoAsyncPhotoAsync(string publicId);
}
