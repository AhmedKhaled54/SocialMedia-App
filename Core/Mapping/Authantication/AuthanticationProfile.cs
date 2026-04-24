using AutoMapper;
using Core.Feature.Authantication.command.Models;
using Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mapping.Authantication
{
    public  class AuthanticationProfile:Profile
    {
        public AuthanticationProfile()
        {
            CreateMap<User, RegisterCommand>()
               .ForMember(d => d.UserName, c => c.MapFrom(s => s.UserName))
               .ForMember(d => d.Email, c => c.MapFrom(s => s.Email))
               .ForMember(d => d.BirthDate, c => c.MapFrom(s => s.BirthDate))
               .ReverseMap();

        }
    }
}
