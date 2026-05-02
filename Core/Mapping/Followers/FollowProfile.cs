using AutoMapper;
using Core.Feature.Follow.Query.Results;
using Core.Resolver.AppUser;
using Core.Resolver.Follower;
using Data.Follower;
using Data.Helper;
using Data.Identity;
using MimeKit.Tnef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mapping.Followers
{
    public  class FollowProfile:Profile
    {
        public FollowProfile()
        {
            CreateMap<User, GetFollowersQueryResult>()
               .ForMember(d => d.UserId, c => c.MapFrom(s => s.Id))
               .ForMember(d => d.UserName, c => c.MapFrom(s => s.UserName))
               .ForMember(d => d.Bio, c => c.MapFrom(s => s.Bio));
               //.ForMember(d => d.Image, c => c.MapFrom<FollowProfileResolver>());

            //CreateMap<FollowerDto, GetFollowersQueryResult>();

        }

    }
}
