using Data.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class Follow
    {
        [Key]
        public int Id { get; set; }
        public int FollowerId { get; set; }
        public User Follower { get; set; }

        public int FollowingId { get; set; }   
        public User Following  { get; set; }


    }
}
