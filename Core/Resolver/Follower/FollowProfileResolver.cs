using AutoMapper;
using Core.Feature.Follow.Query.Results;
using Data.Follower;
using Data.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Resolver.Follower
{
    public class FollowProfileResolver : IValueResolver<User, GetFollowersQueryResult, string>
    {
        private readonly IConfiguration _Config;

        public FollowProfileResolver(IConfiguration config)
        {
            _Config = config;
        }

        public string Resolve(User source, GetFollowersQueryResult destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Image))
                    return _Config["BaseUrl"] + source.Image;
            
            return null; 

        }
    }
}
