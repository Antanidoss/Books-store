using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Models.Registration
{
    public class RegistrationModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string ConfirmPassword { get; set; }
        public string Password { get; set; }
        public bool IsPasrsistent { get; set; }
    }
}
