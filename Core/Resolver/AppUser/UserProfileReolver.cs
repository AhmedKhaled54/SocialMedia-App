using AutoMapper;
using Core.Feature.ApplicationUser.Query.Result;
using Data.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Resolver.AppUser
{
    public class UserProfileReolver : IValueResolver<User, UserDetailsResult, string>
    {
        private readonly IConfiguration _confiq;
        public UserProfileReolver(IConfiguration confiq )
        {
            _confiq = confiq;
        }
        public string Resolve(User source, UserDetailsResult destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Image))
                return  _confiq["BaseUrl"] + source.Image;
            return null;
        }
    }
}
