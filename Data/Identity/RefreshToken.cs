using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Identity
{
  
    public  class RefreshToken
    {
        [Key]
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpireOn { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpireOn;        
        public DateTime CreatedAt { get; set; }
        public DateTime? RevokedOn { get;set; }
        public bool IsActive  =>RevokedOn==null && !IsExpired;

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
