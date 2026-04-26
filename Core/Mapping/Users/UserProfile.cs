using AutoMapper;
using Core.Feature.ApplicationUser.Query.Model;
using Core.Feature.ApplicationUser.Query.Result;
using Core.Feature.Authantication.command.Models;
using Core.Resolver.AppUser;
using Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mapping.Users
{
    public  class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<User, EditProfileCommand>()
               .ForMember(d => d.UserName, c => c.MapFrom(s => s.UserName))
               .ForMember(d => d.Bio, c => c.MapFrom(s => s.Bio))
               .ReverseMap();

            CreateMap<User, UserDetailsResult>()
                .ForMember(d => d.UserName, c => c.MapFrom(s => s.UserName))
                .ForMember(d => d.Email, c => c.MapFrom(s => s.Email))
                .ForMember(d => d.Gender, c => c.MapFrom(s => s.Gender.ToString()))
                .ForMember(d => d.ImgaeProfile,c=>c.MapFrom<UserProfileReolver>());

        }
    }
}
