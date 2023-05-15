using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using WebApplication3.Helpers;
using WebApplication3.Interface;


namespace WebApplication3.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloud;
        public PhotoService(IOptions<CloudinarySettings> config) 
        {
            var account = new Account(
                    config.Value.CloudName,
                    config.Value.ApiKey,
                    config.Value.ApiSecret
                );
            _cloud = new Cloudinary( account );
        }
        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if ( file.Length > 0 )
            {
                using var stream = file.OpenReadStream();
                var uploadPar = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(225).Crop("fill")
                };
                uploadResult = await _cloud.UploadAsync(uploadPar);
            }
            return uploadResult;
        }

       public async Task<DeletionResult> DeletePhotoAsync(string publicUrl)
       {
            var publicId = publicUrl.Split('/').Last().Split('.')[0];
            var deleteParams = new DeletionParams(publicId);
            var results = await _cloud.DestroyAsync(deleteParams);
            return results;
       
       }
}
}
