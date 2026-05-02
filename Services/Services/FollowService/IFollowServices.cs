using Data.Follower;
using Data.Helper;
using Data.Identity;
using MimeKit.Tnef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.FollowService
{
    public  interface IFollowServices
    {
        Task<FollowRequest> GetFollowRequest(int requestid, int receiveid);
        Task<bool> ISFollowing(int senderid ,int receiveid );
        Task<bool> ISPending(int senderid ,int receiveid );
        Task<FollowRequest>AddFollowRequest(int senderid, int receiveid);
        Task<bool> UpdateFollowRequest(FollowRequest request);
        Task<Follow> AddNewFollow(int followinid,int followerid);
        Task<bool> UnFollow(int user, int targetuser);
        Task<bool>RemoveFollower(int user,int followerid);

        IQueryable<User> GetAllFollowers(int userid,string?Search=null );
        IQueryable<User>GetAllFollowing(int userid,string?Search=null );



    }
}
