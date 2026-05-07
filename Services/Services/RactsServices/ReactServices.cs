using Data.Entity;
using Data.Entity.Comments;
using Data.Entity.Posts;
using Data.Enums.Like;
using Data.Helper;
using Infrastructure.Abstract;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Prng;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.RactsServices
{
    public class ReactServices : IReactServices
    {
        private readonly IUnitOfWork _UOW;
        public ReactServices(IUnitOfWork uOW)
        {
            _UOW = uOW;
        }
        public async Task<ReactDto> PostReactionAsync(int PostId, int UserId, LikeType type)
        {
            var response = new ReactDto();

            var post = await _UOW.Repository<Post>().GetById(PostId);
            if (post == null) return null;

            var ExsitReact = await _UOW.Repository<Like>()
                .FindAsync(c => c.PostId == PostId && c.UsertId == UserId);
            if (ExsitReact != null)
            {
                //some type => unreact 
                if (ExsitReact.LikeType == type)
                {
                    _UOW.Repository<Like>().Delete(ExsitReact);
                    //decrease react using dictionary 
                    if (ExsitReact.LikeType > 0)
                        UpdateReaction(post, type, -1);
                    response.Notify = false;
                    await _UOW.Complete();
                    return response;
                }
                var oldtype = ExsitReact.LikeType;
                ExsitReact.LikeType = type;
                UpdateReaction(post, oldtype, -1);
                UpdateReaction(post, type, +1);
                _UOW.Repository<Like>().Update(ExsitReact);
                await _UOW.Complete();
                response.Notify = false;
                return response;
            }
            var NewReact = new Like
            {
                LikeType = type,
                UsertId = UserId,
                PostId = PostId,

            };
            UpdateReaction(post, type, +1);
            await _UOW.Repository<Like>().AddAsync(NewReact);
            await _UOW.Complete();
            response.Notify = true;
            response.PostOwnerId = post.UserId;
            return response;
        }


        public async Task<PostReactDto> GetPostsDetails(int PostId)
        {

            var react = _UOW.Repository<Like>().GetAllpridicated(c => c.PostId == PostId, new[] { "User" }).ToList();
            var count = react.GroupBy(c => c.LikeType).ToDictionary(c => c.Key, c => c.Count());
            var details = react.GroupBy(c => c.LikeType)
                .ToDictionary(c => c.Key,
                c => c.Select(s => new ReactUserDto
                {
                    UserId = s.UsertId,
                    UserName = s.User.UserName
                }).ToList()
                );
            return new PostReactDto
            {
                PostId = PostId,
                TotalCount = react.Count(),
                CountType = count,
                WhosReact = details

            };


        }




        public async Task<(bool notify, int ownerid)> CommentReactAsync(int commentid, int userid, LikeType type)
        {
            var notify = false;
            var comment = await _UOW.Repository<Comment>().GetById(commentid);
            if (comment == null) return (false, 0);

            var ExsitReact = await _UOW.Repository<Like>().FindAsync(c => c.UsertId == userid && c.CommentId == commentid);
            if (ExsitReact != null)
            {
                if (ExsitReact.LikeType == type)
                {
                    _UOW.Repository<Like>().Delete(ExsitReact);
                    //if (ExsitReact.LikeType>0)
                    await _UOW.Complete();
                    return (false, 0);

                }
                var oldtype = ExsitReact.LikeType;
                ExsitReact.LikeType = type;
                _UOW.Repository<Like>().Update(ExsitReact);
                await _UOW.Complete();
                return (false, 0);



            }
            var NewReact = new Like
            {
                UsertId = userid,
                LikeType = type,
                CommentId = commentid,

            };

            await _UOW.Repository<Like>().AddAsync(NewReact);
            await _UOW.Complete();
            return (true, comment.UserId);


        }



        private void UpdateReaction(Post post, LikeType type, int delta)
        {
            if (_reactionMap.TryGetValue(type, out var action))
            {
                action(post, delta);
            }
        }

        public async Task<CommentReactDto> GetCommentDetails(int CommentId)
        {

            var react = _UOW.Repository<Like>()
                .GetAllpridicated(c => c.CommentId == CommentId, new[] {"User"} );

            //grouping typecount
            var count = react.GroupBy(c => c.LikeType).ToDictionary(c => c.Key, c => c.Count());
            //gruorp details type userreacts

            var details = react.GroupBy(c => c.LikeType)
                .ToDictionary(c => c.Key,
                c => c.Select(d => new ReactUserDto
                {
                    UserId = d.UsertId,
                    UserName = d.User.UserName
                }).ToList());
            return new CommentReactDto
            {
                CommentId = CommentId,
                TotalCount = count.Count(),
                ReactsCount = count,
                WhosReacts = details
            };


        }

        private readonly Dictionary<LikeType, Action<Post, int>> _reactionMap = new()
        {
            { LikeType.Like,  (p, v) => p.LikesCount += v },
            { LikeType.Love,  (p, v) => p.LoveCount += v },
            { LikeType.Haha,  (p, v) => p.HahaCount += v },
            { LikeType.Wow,   (p, v) => p.WowCount += v },
            { LikeType.Sad,   (p, v) => p.SadCount += v },
            { LikeType.Angry, (p, v) => p.AngryCount += v }
        };
    }
}
