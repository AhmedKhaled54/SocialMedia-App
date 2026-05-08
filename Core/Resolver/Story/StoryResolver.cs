using AutoMapper;
using Core.Feature.Story.Query.Results;
using Data.Entity.Stories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Resolver.Story
{
    public class StoryResolver : IValueResolver<StoryMedia, GetSyoryQueryResult, string>
    {
        private readonly IConfiguration _confiq;
        public StoryResolver(IConfiguration confiq)
        {
            _confiq = confiq;
        }
        public string Resolve(StoryMedia source, GetSyoryQueryResult destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.Url))
                return string.Empty;
            return _confiq["BaseUrl"] + source.Url;
        }
    }
}
