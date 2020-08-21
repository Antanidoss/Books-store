using BooksStore.Core.AppUserModel;
using BooksStore.Service.DTO;
using BooksStore.Service.Interfaces.Identity;
using BooksStore.Web.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BooksStore.Web.Models.User.CurUser
{
    public class CurrentUser : ICurrentUser
    {       
        public CurrentUser(IUserManagerService userManagerService)
        {
            UserManagerService = userManagerService;       
        }
        IUserManagerService UserManagerService { get; set; }
        public string AppUserId { get; set; }
        public bool IsAuthenticated { get; set; }
        public int BasketId { get; set; }

        public async Task<AppUserDTO> GetCurrentUser(HttpContext httpContext)
        {
            return (await UserManagerService.FindAppUserByIdAsync(httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier))).AppUserDTO;
        }
    }
}
