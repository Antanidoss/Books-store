using BooksStore.Common;
using BooksStore.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace BooksStore.Services.Implementation.Services
{
    internal static class IdentityResultExtensions
    {
        public static Result ToApplicationResult(this IdentityResult result)
        {
            return result.Succeeded
                ? Result.Success()
                : Result.Failure(result.Errors.Select(e => e.Description));
        }

        public static Result EmailAreadyUse()
        {
            return Result.Failure(new string[] { "Введенная эл.почта уже используется" });
        }

        public static Result AppUserNotFound()
        {
            return Result.Failure(new string[] { "Пользователь не найден" });
        }

        public static Result IncorrectlyEnteredEmailOrPassword()
        {
            return Result.Failure(new string[] { "Неправильно введён логин или пароль" });
        }

        public static Result RoleNotFound()
        {
            return Result.Failure(new string[] { "Роль не найдена" });
        }
    }
}