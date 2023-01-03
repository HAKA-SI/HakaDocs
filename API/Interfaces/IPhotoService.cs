

using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace API.Interfaces
{
  public interface IPhotoService
  {
   ImageUploadResult AddUserPhotoFile(IFormFile photoFile);
    ImageUploadResult AddProductPhotoFile(IFormFile photoFile);
    DeletionResult DeletePhotoAsync(string publicId);

  }
}