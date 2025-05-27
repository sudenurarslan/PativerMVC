// IFileService.cs
namespace PativerMVC.Services
{
    public interface IFileService
    {
        Task<string> SaveImageAsync(IFormFile imageFile);
        void DeleteImage(string imagePath);
    }
}


