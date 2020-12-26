using AutoMapper;
using BooksStore.Core.Entities;
using BooksStore.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooksStore.Services.Profiles
{
    public class CommentDTOProfile : Profile
    {
        public CommentDTOProfile()
        {
            CreateMap<Comment, CommentDTO>()
                .ForMember(dto => dto.AppUserName, conf => conf.MapFrom(o => o.AppUser.UserName));
            CreateMap<CommentDTO, Comment>();
        }        
    }
}
