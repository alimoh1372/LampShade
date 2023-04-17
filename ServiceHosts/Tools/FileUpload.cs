using System.IO;
using _0_Framework.Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ServiceHosts.Tools
{
    public class FileUpload :IFileUpload
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUpload(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string UploadFile(IFormFile file, string path)
        {
            if (file ==null)
            {
                return "";
            }

            string rootDirectory = _webHostEnvironment.WebRootPath;
            string baseDirectory = path;
            string pathDirectory = $"{rootDirectory}/{baseDirectory}/";
            string fileName = file.FileName;
            string _path = $"{pathDirectory}/{fileName}";
            if (!Directory.Exists(pathDirectory))
            {
                Directory.CreateDirectory(pathDirectory);
            }

            using FileStream output= System.IO.File.Create(_path);
            file.CopyToAsync(output);

            return $"{path}//{fileName}";

        }
    }
}