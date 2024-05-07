using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using RunGroupWebApp.Helpers;
using RunGroupWebApp.interfaces;

namespace RunGroupWebApp.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        public PhotoService(IOptions<CloudinarySettings> config)
        {
            _cloudinary = new Cloudinary(
                new Account(
                    config.Value.CloudName,
                    config.Value.ApiKey,
                    config.Value.ApiSecret
                    ));
        }
        async Task<ImageUploadResult> IPhotoService.AddPhotoAsync(IFormFile file)
        {
            var upLoadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var upLoadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };
                upLoadResult = await _cloudinary.UploadAsync(upLoadParams);
            }
            return upLoadResult;
        }

        async Task<DeletionResult> IPhotoService.DeletePhotoAsync(string publicId)
        {
            var deleteResult = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteResult);

            return result;
        }
    }
}
