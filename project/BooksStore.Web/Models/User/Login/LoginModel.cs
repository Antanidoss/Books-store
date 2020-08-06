using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Models.Login
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsParsistent { get; set; }
    }
}
