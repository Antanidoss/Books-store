using AutoMapper;
using BooksStore.Core.AuthorModel;
using BooksStore.Service.DTO;
using System.Collections.Generic;

namespace BooksStore.Service.Profiles
{
    public class AuthorDTOProfile: Profile
    {
        public AuthorDTOProfile()
        {
            CreateMap<Author, AuthorDTO>();
            CreateMap<AuthorDTO, Author>();
        }
    }
}
