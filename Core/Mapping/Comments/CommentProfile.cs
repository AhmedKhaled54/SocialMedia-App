using AutoMapper;
using Core.Feature.Comments.Query.Models;
using Core.Feature.Comments.Query.Results;
using Data.Entity.Comments;
using Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mapping.Comments
{
    public  class CommentProfile:Profile
    {
        public CommentProfile()
        {
            CreateMap<CommentReactDto, GetReactCommentDetailsQueryResult>()
                .ForMember(d => d.CommentId, c => c.MapFrom(s => s.CommentId))
                .ForMember(d => d.TotalCountReacts, c => c.MapFrom(s => s.TotalCount))
                .ForMember(d => d.ReactCount, c => c.MapFrom(s => s.ReactsCount))
                .ForMember(d => d.ReactDetails, c => c.MapFrom(s => s.WhosReacts));

            CreateMap<ReactUserDto, CommentReactUser>();

            CreateMap<Comment, GetCommentsQueryResult>()
                .ForMember(c => c.CommentId, c => c.MapFrom(c => c.Id))
                .ForMember(c => c.CreatedAt, c => c.MapFrom(c => c.CreatedAt))
                .ForMember(c => c.Content, c => c.MapFrom(c => c.Content))
                .ForMember(c => c.Medias, c => c.MapFrom(c => c.Media))
                .ForMember(c => c.ReactsCount, c => c.Ignore())
                .ForMember(c=>c.ReactDetails,c=>c.Ignore());//in handler mapping 
        
            
            CreateMap<CommentMedia, GetCommentMediaQueryResult>();
            




            //MapFrom(c => c.Likes
            //    .GroupBy(c => c.LikeType)
            //    .ToDictionary(c => c.Key, c => c.Count())))
            //.ForMember(c => c.ReactDetails, c => c.MapFrom(c => c.Likes
            //.GroupBy(c => c.LikeType)
            //.ToDictionary(c => c.Key, c => c.Select(s => new GetCommentUserQueryResult
            //{
            //    UserId = s.UsertId,
            //    UserName = s.User.UserName
            //}).ToList())));

        }
    }
}
