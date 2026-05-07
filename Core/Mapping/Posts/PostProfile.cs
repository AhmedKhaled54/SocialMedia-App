using AutoMapper;
using Core.Feature.conversation.Query.Results;
using Core.Feature.Posts.Command.Models;
using Core.Feature.Posts.Query.Result;
using Data.Entity.Posts;
using Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mapping.Posts
{
    public  class PostProfile:Profile
    {
        public PostProfile()
        {
            CreateMap<Post, GetpostsFeedQueryResult>()
               .ForMember(d => d.PostId, c => c.MapFrom(s => s.Id))
               .ForMember(d => d.Caption, c => c.MapFrom(s => s.Caption))
            .ForMember(d => d.UserName, c => c.MapFrom(s => s.user.UserName))
            .ForMember(d => d.MediaUrls, c => c.MapFrom(s => s.Media))
            .ForMember(d => d.CommentsCount, c => c.MapFrom(s => s.user.Comments.Count()));

            CreateMap<PostMedia, MedaiQueryResult>();

            CreateMap<dynamic, GetpostsFeedQueryResult>();//get data is a object 


            CreateMap<PostReactDto, GetPostReactDetailQueryResult>()
                .ForMember(d => d.TotalCountReact, c => c.MapFrom(c => c.TotalCount))
                .ForMember(d => d.ReactCount, c => c.MapFrom(c => c.CountType))
                .ForMember(d => d.ReactDetails, c => c.MapFrom(c => c.WhosReact));
            CreateMap<ReactUserDto, GetPostUserDetails>();


        }
    }
}
