using API.Helpers;
using API.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace API.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public PhotoService(IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _cloudinaryConfig = cloudinaryConfig;

            Account acc = new Account(
             _cloudinaryConfig.Value.CloudName,
             _cloudinaryConfig.Value.ApiKey,
             _cloudinaryConfig.Value.ApiSecret
         );

            _cloudinary = new Cloudinary(acc);
        }

        public ImageUploadResult AddUserPhotoFile(IFormFile photoFile)
        {

            var uploadResult = new ImageUploadResult();
            using (var stream = photoFile.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(photoFile.Name, stream),
                    Transformation = new Transformation().Width(500).Height(500).FetchFormat("auto").Quality("auto")
                                                       .Crop("fill").Gravity("face")
                };
                uploadParams.Folder = "FanyaSoft/";
                return _cloudinary.Upload(uploadParams);
            }

        }

        public ImageUploadResult AddProductPhotoFile(IFormFile photoFile)
        {
            var uploadResult = new ImageUploadResult();
            using (var stream = photoFile.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(photoFile.Name, stream),
                    Transformation = new Transformation().Width(300).Height(300).FetchFormat("auto").Quality("auto")
                                                       .Crop("fill").Gravity("face")
                };
                uploadParams.Folder = "HakaDocs/";

                return _cloudinary.Upload(uploadParams);
            }
        }

        public DeletionResult DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = _cloudinary.Destroy(deleteParams);
            return result;
        }

        // public ImageUploadResult AddUserPhotoFile(IFormFile photoFile)
        // {
        //     throw new NotImplementedException();
        // }

        // public ImageUploadResult AddProductPhotoFile(IFormFile photoFile)
        // {
        //     throw new NotImplementedException();
        // }

        // DeletionResult IPhotoService.DeletePhotoAsync(string publicId)
        // {
        //     throw new NotImplementedException();
        // }
    }
}