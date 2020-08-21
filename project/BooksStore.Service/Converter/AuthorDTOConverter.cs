using AutoMapper;
using BooksStore.Core.AuthorModel;
using BooksStore.Service.DTO;
using System.Collections.Generic;

namespace BooksStore.Service.Converter
{
    internal static class AuthorDTOConverter
    {
        public static AuthorDTO ConvertToAuthorDTO(Author author)
        {
            var map = new MapperConfiguration(cfg => cfg.CreateMap<Author, AuthorDTO>()).CreateMapper();
            return map.Map<Author, AuthorDTO>(author);
        }
        public static IEnumerable<AuthorDTO> ConvertToAuthorDTO(IEnumerable<Author> authors)
        {
            var map = new MapperConfiguration(cfg => cfg.CreateMap<Author, AuthorDTO>()).CreateMapper();
            return map.Map<IEnumerable<Author>, IEnumerable<AuthorDTO>>(authors);
        }

        public static Author ConvertToAuthor(AuthorDTO authorDTO)
        {
            var map = new MapperConfiguration(cfg => cfg.CreateMap<AuthorDTO, Author>()).CreateMapper();
            return map.Map<AuthorDTO, Author>(authorDTO);
        }
    }
}
