using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FilesServices
{
    public  interface IFileServices
    {
        Task<string> UploadImage(IFormFile file, string folder);
        Task<bool> RemoveImage(string FilePath);
    }
}
