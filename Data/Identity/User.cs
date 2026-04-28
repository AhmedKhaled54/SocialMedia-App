using Data.Entity;
using Data.Entity.Comments;
using Data.Entity.Posts;
using Data.Entity.Stories;
using Data.Enums.Authantication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Identity
{
    public class User : IdentityUser<int>
    {
        public string? Bio { get; set; }
        public DateOnly BirthDate { get; set; }
        public string? Image { get; set; }
        public Gender Gender { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; }
        public ICollection<Post> Posts { get; set; }=new List<Post>();
        public ICollection<Follow> Following { get; set; }=new List<Follow>();
        public ICollection<Follow> Followers { get; set; } =new List<Follow>();

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection <Story> Stories { get; set; }= new List<Story>();
        public ICollection<Like>  Likes { get; set; } =new HashSet<Like>();
        
        public ICollection<PrivateMessage> SentMessages { get; set; } = new List<PrivateMessage>();
        public ICollection<PrivateMessage> ReceivedMessages { get; set; } = new List<PrivateMessage>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>(); //recive 
        public ICollection<Notification> SendeNotication { get; set; } = new List<Notification>();//Actor  

    }
}
