using Data.Entity.Comments;
using Data.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity.Posts
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string? Caption { get; set; }
        public DateTime CreatedAt { get; set; }=DateTime.UtcNow;
        public ICollection<PostMedia> Media { get; set; }=new List<PostMedia>();
        public ICollection<Comment> Comments { get; set; }=new List<Comment>();
        public int UserId { get; set; }
        public User user { get; set; }
        public ICollection<Like> Likes { get; set; }=new List<Like>();
        public int LikesCount { get; set; } = 0;
        public int LoveCount { get; set; } = 0;
        public int HahaCount { get; set; } = 0;
        public int WowCount { get; set; } = 0;
        public int SadCount { get; set; } = 0;
        public int AngryCount { get; set; } = 0;

    }
}
