using Data.Entity.Posts;
using Data.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity.Comments
{
    public class Comment
    {

        public int Id { get; set; }
        public string? Content  { get; set; }
        public DateTime CreatedAt { get; set; }=DateTime.UtcNow;
        public int UserId { get; set; }
        public int PostId { get; set; }
        public User User { get; set; }
        public Post Post { get; set; }

        public ICollection<Like> Likes { get; set; }=new List<Like>();
        public ICollection<CommentMedia> Media { get; set; }=new List<CommentMedia>();
    }
}
