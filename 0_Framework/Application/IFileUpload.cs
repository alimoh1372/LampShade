using Microsoft.AspNetCore.Http;

namespace _0_Framework.Application
{
    public interface IFileUpload
    {
        string UploadFile(IFormFile file, string path);
    }
}