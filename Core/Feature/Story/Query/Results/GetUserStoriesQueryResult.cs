using Core.Feature.Story.Query.Models;
using Data.Enums.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Story.Query.Results
{
    public  class GetUserStoriesQueryResult
    {
        public string UserId { get; set; }
        public string UserName  { get; set; }
        public string Image { get;set; }
        public List<GetSyoryQueryResult>? Media {  get; set; }
    }

    public class GetSyoryQueryResult
    {
        public string Url { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpireAt { get; set; }
        public MedaiType Type { get; set; }

    }
}
