using Microsoft.AspNetCore.Identity;

namespace Bloggie.Web.Data.Repository.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}
