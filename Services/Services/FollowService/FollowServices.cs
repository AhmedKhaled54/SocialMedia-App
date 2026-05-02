using Data.Enums.Follow;
using Data.Follower;
using Data.Helper;
using Data.Identity;
using Infrastructure.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Services.Services.FollowService
{
    public class FollowServices : IFollowServices
    {
        private readonly IUnitOfWork _UOW;
        public FollowServices(IUnitOfWork uOW)
        {
            _UOW = uOW;
        }
        public async Task<FollowRequest> AddFollowRequest(int senderid, int receiveid)
        {
            var request = new FollowRequest
            {
                SenderId = senderid,
                ReceiveId = receiveid
            };
            await _UOW.Repository<FollowRequest>().AddAsync(request);
            await _UOW.Complete();
            return request;
            
        }

        public async Task<Follow> AddNewFollow(int followinid, int followerid)
        {
            var follow = new Follow
            {
                FollowerId = followerid,
                FollowingId = followinid,
            };

             await _UOW.Repository<Follow>().AddAsync(follow);
             await _UOW.Complete();
            return follow;
        
        }

        public async Task<FollowRequest> GetFollowRequest(int requestid, int receiveid)
        {
            var request = await _UOW.Repository<FollowRequest>()
                .FindAsync(c => 
                c.Id == requestid &&
                c.ReceiveId == receiveid&&
                c.Status==FollowStatus.Pending);
            return request;


        }

        public async Task<bool> ISFollowing(int senderid, int receiveid)
            =>await _UOW.Repository<Follow>().IsAny(c=>c.FollowerId==senderid&&c.FollowingId==receiveid);

        public async  Task<bool> ISPending(int senderid, int receiveid)
            => await _UOW.Repository<FollowRequest>()
                .IsAny(c =>
                c.SenderId == senderid &&
                c.ReceiveId == receiveid &&
                c.Status == FollowStatus.Pending);

       

        public async Task<bool> UpdateFollowRequest(FollowRequest request)
        {
            _UOW.Repository<FollowRequest>().Update(request);
            await _UOW.Complete();
            return true;
        }
        public async Task<bool> UnFollow(int user, int targetuser)
        {
            var follow = await _UOW.Repository<Follow>()
                .FindAsync(c => c.FollowerId == user && c.FollowingId == targetuser);
            if (follow != null)
            {
                _UOW.Repository<Follow>().Delete(follow);
                await _UOW.Complete();
                return true;
            }
            return false;

        }

        public async Task<bool> RemoveFollower(int user, int followerid)
        {
            var Follower = await _UOW.Repository<Follow>()
                .FindAsync(c => c.FollowingId == user && c.FollowerId == followerid);
            if (Follower != null)
            {
                _UOW.Repository<Follow>().Delete(Follower);
                await _UOW.Complete();
                return true;

            }
            return false;
        }

        public IQueryable<User> GetAllFollowers(int userid, string? Search=null)
        {
            var follower = _UOW.Repository<Follow>()
                .GetAllpridicated(c => c.FollowingId == userid)
                .Select(c=>c.Follower).AsQueryable();

            if (!string.IsNullOrEmpty(Search))
            {
                follower= follower.Where(c => EF.Functions.Like(c.UserName, $"%{Search.Trim()}%"));
            }
            return follower;

        }

        public IQueryable<User> GetAllFollowing(int userid, string? Search = null)
        {
          var following= _UOW.Repository<Follow>()
                .GetAllpridicated(c=>c.FollowerId==userid)
                .Select(c=>c.Following).AsQueryable();
            if (!string.IsNullOrEmpty(Search))
            {

                following = following.Where(c => EF.Functions.Like(c.UserName, $"%{Search}%"));
            }

            return following;
        }
    }
}
