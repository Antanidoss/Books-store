using AutoMapper;
using BooksStore.Core.Entities;
using BooksStore.Services.DTO.Comment;

namespace BooksStore.Services.Profiles
{
    internal sealed class CommentDTOProfile : Profile
    {
        public CommentDTOProfile()
        {
            CreateMap<Comment, CommentDTO>()
                .ForMember(dto => dto.AppUserName, conf => conf.MapFrom(o => o.AppUser.UserName));
            CreateMap<CommentDTO, Comment>();
        }        
    }
}
