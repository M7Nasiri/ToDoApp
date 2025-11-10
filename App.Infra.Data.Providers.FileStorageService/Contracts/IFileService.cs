using Microsoft.AspNetCore.Http;

namespace App.Infra.Data.FileStorageService.Contracts
{
    public interface IFileService
    {
        //public string Upload(string filePath, string folder);
        //public void Delete(string fileName);
        string Upload(Stream fileStream, string fileName, string folder);
        string Upload(IFormFile file, string folder);
        void Delete(string fileName);
    }
}
