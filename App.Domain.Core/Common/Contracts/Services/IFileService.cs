using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Common.Contracts.Services
{
    public interface IFileService
    {
        //public string Upload(string filePath, string folder);
        //public void Delete(string fileName);
        string Upload(Stream fileStream, string fileName, string folder);
       // string Upload(IFormFile file, string folder);
        void Delete(string fileName);
    }
}
