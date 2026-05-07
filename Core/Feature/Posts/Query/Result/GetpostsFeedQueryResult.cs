using Data.Enums.Media;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Org.BouncyCastle.Bcpg.Sig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Posts.Query.Result
{
    public  class GetpostsFeedQueryResult
    {
        public int PostId { get; set; }
        public string? Caption { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserName { get; set; }
        public string UserImage { get; set; }
        public List<MedaiQueryResult>? MediaUrls { get; set; } = new();
        public int LikesCount { get; set; } 
        public int LoveCount { get; set; } 
        public int HahaCount { get; set; } 
        public int WowCount { get; set; } 
        public int SadCount { get; set; } 
        public int AngryCount { get; set; }
        public int CommentsCount { get; set; }

    }

    public class MedaiQueryResult
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public MedaiType Type { get; set; }
    }

}
