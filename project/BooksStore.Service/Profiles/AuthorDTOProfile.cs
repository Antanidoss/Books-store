using AutoMapper;
using BooksStore.Core.Entities;
using BooksStore.Services.DTO.Author;

namespace BooksStore.Services.Profiles
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