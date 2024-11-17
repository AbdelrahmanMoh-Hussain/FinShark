using api.Models;
using api.Dtos.Stock;
﻿using AutoMapper;
using api.Dtos.Comment;


namespace api.Mappers
{
    public class MappingProfile: Profile
	{
        public MappingProfile()
        {
            CreateMap<Stock, StockDto>();
            CreateMap<Comment, CommentDto>();
            CreateMap<UpdateCommentDto, Comment>();
            CreateMap<CreateCommentDto, Comment>();
            CreateMap<StockRequestDto, Stock>();
            
        }
    }
}