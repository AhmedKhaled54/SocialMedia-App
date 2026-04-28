using Data.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity.Stories
{
    public class Story
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.UtcNow;
        public DateTime ExprieAt { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<StoryMedia> Media { get; set; }= new List<StoryMedia>();//cascade behavior 
    }
}
