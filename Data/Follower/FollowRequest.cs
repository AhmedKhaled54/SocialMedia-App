using Data.Enums.Follow;
using Data.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Follower
{
    public  class FollowRequest
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public User Sender {  get; set; }
        public int ReceiveId {  get; set; }
        public User Receive { get; set; }
        public FollowStatus Status { get; set; }=FollowStatus.Pending;
        public DateTime CreatedAt {  get; set; }=DateTime.UtcNow;
    }
}
