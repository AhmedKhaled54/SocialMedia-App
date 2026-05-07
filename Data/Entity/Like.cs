using Data.Entity.Comments;
using Data.Entity.Posts;
using Data.Enums.Like;
using Data.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public  class Like
    {
        public int Id { get; set; }
        public int UsertId { get; set; }
        public int? PostId { get; set; }
        public int? CommentId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public LikeType LikeType { get; set; }=LikeType.Like;
        public User User { get; set; }
        public Post? Post { get; set; }
        public Comment ?Comment { get; set; }
 

    }
}
