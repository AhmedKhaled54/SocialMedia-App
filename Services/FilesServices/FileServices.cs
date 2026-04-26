using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FilesServices
{
    public class FileServices : IFileServices
    {
        private readonly IWebHostEnvironment webHost;
        private new List<string> allowextention = new List<string>() { ".jpg", ".png", "jpeg" };
        private long maxsizeimage = 109951163;//2MB
        public FileServices(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
        }

        

        public async Task<string> UploadImage(IFormFile file, string folder)
        {
            if (!allowextention.Contains(Path.GetExtension(file.FileName.ToLower())))
                throw new Exception("Must Be Image Allawed (.png) / (.jpg) / (.jpeg) ! ");

            if (file.Length > maxsizeimage)
                throw new Exception("ax size is 2MB");
            var filename = Guid.NewGuid().ToString() + file.FileName;
            var pathfile  = Path.Combine(webHost.WebRootPath,folder,filename);
            using var stream = new FileStream(pathfile, FileMode.Create);
            await file.CopyToAsync(stream);
            return $"{folder}/{filename}";
        }



        public  async  Task<bool> RemoveImage(string FilePath)
        {
            if (string.IsNullOrEmpty(FilePath))
                return false;
            var fullPath =Path.Combine(webHost.WebRootPath,FilePath);

            if (!File.Exists(fullPath))
                return false;
            File.Delete(fullPath);
            return true;

        }
    }
}
