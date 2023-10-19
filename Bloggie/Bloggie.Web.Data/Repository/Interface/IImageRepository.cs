using Microsoft.AspNetCore.Http;

namespace Bloggie.Web.Data.Repository.Interface
{
    public interface IImageRepository
    {
        Task<string> UploadAsync(IFormFile file);
    }
}
