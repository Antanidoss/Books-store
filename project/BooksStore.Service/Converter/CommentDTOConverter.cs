using AutoMapper;
using BooksStore.Core.CommentModel;
using BooksStore.Service.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooksStore.Service.Converter
{
    internal static class CommentDTOConverter
    {
        public static CommentDTO ConvertToCommentDTO(Comment comment)
        {
            var map = new MapperConfiguration(cfg => cfg.CreateMap<Comment, CategoryDTO>()).CreateMapper();
            return map.Map<Comment, CommentDTO>(comment);
        }
        public static IEnumerable<CommentDTO> ConvertToCommentDTO(IEnumerable<Comment> comments)
        {
            var map = new MapperConfiguration(cfg => cfg.CreateMap<Comment, CategoryDTO>()).CreateMapper();
            return map.Map<IEnumerable<Comment>, IEnumerable<CommentDTO>>(comments);
        }
        public static Comment ConvertToComment(CommentDTO commentDTO)
        {
            var map = new MapperConfiguration(cfg => cfg.CreateMap<CommentDTO, Comment>()).CreateMapper();
            return map.Map<CommentDTO, Comment>(commentDTO);
        }
    }
}
