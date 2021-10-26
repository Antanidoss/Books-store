using BooksStore.Services.DTO;
using BooksStore.Services.DTO.AppUser;
using BooksStore.Services.Interfaces.IdentityServices;
using BooksStore.Web.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BooksStore.Web.Common.CurUser
{
    public class CurrentUser : ICurrentUser
    {       
        public CurrentUser(IUserService userManagerService)
        {
            UserManagerService = userManagerService;       
        }
        private IUserService UserManagerService { get; set; }

        public async Task<AppUserDTO> GetCurrentUser(HttpContext httpContext)
        {
            var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await UserManagerService.FindAppUserByIdAsync(userId);

            return result.AppUserDTO;
        }
    }
}
