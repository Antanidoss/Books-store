using BooksStore.Core.AppUserModel;
using BooksStore.Web.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BooksStore.Web.Models.User.CurUser
{
    public class CurrentUser : ICurrentUser
    {
        UserManager<AppUser> UserManager { get; set; }        

        public CurrentUser(UserManager<AppUser> userManager)
        {
            UserManager = userManager;
        }

        public async Task<AppUser> GetCurrentAppUser(HttpContext httpContext) => await UserManager.GetUserAsync(httpContext.User);
    }
}
