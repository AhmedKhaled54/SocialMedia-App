using AutoMapper;
using Core.Feature.Authorization.Command.Models;
using Core.Feature.Authorization.Results;
using Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mapping.Authorization
{
    public  class AuthorizeProfile:Profile
    {
        public AuthorizeProfile()
        {
            CreateMap<Role, RoleDeatailsResult>()
                .ForMember(d => d.RoleID, c => c.MapFrom(s => s.Id))
                .ForMember(d => d.RoleName, c => c.MapFrom(s => s.Name));

            CreateMap<Role, EditRoleCommand>()
                .ForMember(d => d.RoleID, c => c.MapFrom(s => s.Id))
                .ForMember(d => d.RoleName, c => c.MapFrom(s => s.Name)).ReverseMap();


        }
    }
}
