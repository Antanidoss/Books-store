﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BooksStore.Service.DTO
{
    public class AppUserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int BasketId { get; set; }
    }
}
