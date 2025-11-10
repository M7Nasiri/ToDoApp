using App.Domain.Core.FileAgg.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.FileAgg
{
    public class FileService : IFileService
    {
        public string Upload(Stream fileStream, string fileName, string folder)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", folder);

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueName = Guid.NewGuid() + Path.GetExtension(fileName);

            var filePath = Path.Combine(uploadsFolder, uniqueName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                fileStream.CopyTo(stream);
            }

            return Path.Combine("/Files", folder, uniqueName);
        }

        public void Delete(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return;

            fileName = fileName.TrimStart('/', '\\');

            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);

            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }
    }
}
