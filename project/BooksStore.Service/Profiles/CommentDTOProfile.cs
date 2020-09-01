using AutoMapper;
using BooksStore.Core.CommentModel;
using BooksStore.Service.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooksStore.Service.Profiles
{
    public class CommentDTOProfile : Profile
    {
        public CommentDTOProfile()
        {
            CreateMap<Comment, CategoryDTO>();
            CreateMap<CommentDTO, Comment>();
        }        
    }
}
