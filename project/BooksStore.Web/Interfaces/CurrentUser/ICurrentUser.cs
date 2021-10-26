using BooksStore.Services.DTO;
using BooksStore.Services.DTO.AppUser;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BooksStore.Web.Interfaces
{
    public interface ICurrentUser
    {
        Task<AppUserDTO> GetCurrentUser(HttpContext httpContext);
    }
}