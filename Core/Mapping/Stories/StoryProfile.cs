using AutoMapper;
using Core.Feature.Story.Query.Results;
using Core.Resolver.Story;
using Data.Entity.Stories;
using Data.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mapping.Stories
{
    public  class StoryProfile:Profile
    {
       
        public StoryProfile()
        {
          
            CreateMap<User, GetUserStoriesQueryResult>()
               .ForMember(d => d.UserId, c => c.MapFrom(s => s.Id))
               .ForMember(d => d.UserName, c => c.MapFrom(s => s.UserName))
               .ForMember(d => d.Image, c => c.MapFrom(s => s.Image))
               .ForMember(d => d.Media, c => c.MapFrom(s => s.Stories.SelectMany(c=>c.Media)));

            CreateMap<StoryMedia, GetSyoryQueryResult>()
                .ForMember(d => d.ExpireAt, c => c.MapFrom(s => s.Story.ExprieAt))
                .ForMember(d => d.CreatedAt, c => c.MapFrom(s => s.Story.CreatedAt))
               // .ForMember(d => d.Url, c => c.MapFrom(s => s.Url))
                .ForMember(d => d.Url, c =>c.MapFrom<StoryResolver>())
                .ForMember(d => d.Type, c => c.MapFrom(s => s.Type));

            //storyById No grouping 
            CreateMap<Story, GetUserStoriesQueryResult>()
              .ForMember(d => d.UserId, c => c.MapFrom(s => s.User.Id))
              .ForMember(d => d.UserName, c => c.MapFrom(s => s.User.UserName))
              .ForMember(d => d.Image, c => c.MapFrom(s => s.User.Image))
              .ForMember(d => d.Media, c => c.MapFrom(s =>s.Media));
        }
    }
}
