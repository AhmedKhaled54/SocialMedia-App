using Data.Entity.Posts;
using Data.Enums.Media;
using Data.Follower;
using Infrastructure.Abstract;
using Infrastructure.Specification.PostSpecifications;
using MailKit;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MimeKit.Cryptography;
using Org.BouncyCastle.Crypto.Prng;
using Services.FilesServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.PostsServices
{
    public class PostServices : IPostServices
    {
        private readonly IUnitOfWork _UOW;
        private readonly IFileServices _fileServices;

        public PostServices(IUnitOfWork uOW, IFileServices fileServices)
        {
            _UOW = uOW;
            _fileServices = fileServices;
        }

        public async Task<Post> CreatePost(int userid, string? caption,List<PostMedia>?media )
        {
            var post = new Post
            {
                UserId = userid,
                Caption = caption,
                CreatedAt = DateTime.UtcNow
            };
            if (media != null && media.Any())
                post.Media = media;
            
            await _UOW.Repository<Post>().AddAsync(post);
            await _UOW.Complete();
            return post;
        }


        public async Task<List<PostMedia>> PostMediaProcess(List<IFormFile> media)
        {
            var result = new List<PostMedia>();

            foreach (var mediaItem in media)
            {

                var path = Path.GetExtension(mediaItem.FileName.ToLower());
                var type = path == ".mp4" ? MedaiType.Video : MedaiType.Image;
                var folder = path == ".mp4" ? "Posts/Videos" : "Posts/Images";

                var url = await _fileServices.UploadImage(mediaItem, folder);
                result.Add(new PostMedia
                {
                    Type = type,
                    CreatedAt = DateTime.UtcNow,
                    Url = url
                });

            }


            return result;

        }




        public IQueryable<Post> GetFeedPost(int userid)
        {
            var followingid = _UOW.Repository<Follow>().GetTable(c => c.FollowerId == userid).Select(c => c.FollowingId).ToList();

            //var posts = _UOW.Repository<Post>()
            //    .GetAllpridicated(c => followingid.Contains(c.UserId)
            //    || c.UserId == userid, new[] { "user ", "Media" })
            //    .OrderByDescending(c=>c.CreatedAt)
            //    .AsQueryable();

            var p = _UOW.Repository<Post>().GetTable(c => followingid.Contains(c.UserId) || c.UserId == userid)
                .Include(c => c.user).Include(c => c.Media).AsQueryable();

            return p;

        }

        public async Task<IEnumerable<Post>> GetFeedPostList(int userid)
        {

            var followingid = _UOW.Repository<Follow>().GetTable(c => c.FollowerId == userid).Select(c => c.FollowingId).ToList();

            //var posts = _UOW.Repository<Post>()
            //    .GetAllpridicated(c => followingid.Contains(c.UserId)
            //    || c.UserId == userid, new[] { "user ", "Media" })
            //    .OrderByDescending(c=>c.CreatedAt)
            //    .AsQueryable();

            var p =await  _UOW.Repository<Post>().GetTable(c => followingid.Contains(c.UserId) || c.UserId == userid)
                .Include(c => c.user).Include(c => c.Media).ToListAsync();

            return p;

        }

        public IQueryable<Post> getPostSepecification(PostSpecification post)
        {
            var following = _UOW.Repository<Follow>().GetTable(c => c.FollowerId == post.userid).Select(c => c.FollowingId).ToList();

            post.FollowingId =following;
            var specs = new PostWithUserAndMedia(post);
            
            var query = _UOW.Repository<Post>().GetEntitiesWithSpecs(specs);
            return query;
            
        }

        public async Task<Post> GetPostById(int PostId)
        {
            //var post =await  _UOW.Repository<Post>().GetTableAsNoTracking().Include(c=>c.Media).FirstOrDefaultAsync(c=>c.Id == PostId);
            //if (post == null)
            //    return null;
            //return post

            var specs =new PostWithUserAndMedia(PostId);
            var post = await  _UOW.Repository<Post>().GetEntityByIdSepcs(specs);
            return post;




        }

        public async Task<bool> UpdatePost(Post post)
        {
            _UOW.Repository<Post>().Update(post);
            await _UOW.Complete();
            return true;

        }
        public async  Task <bool> DeletePost(Post post)
        {
            _UOW.Repository<Post>().Delete(post);
            await _UOW.Complete();
            return true;

        }

        public async Task<bool> DeletePostMedia(List<PostMedia>media)
        {
            _UOW.Repository<PostMedia>().RemoveRange(media);
            await _UOW.Complete();
            return true;
        }
    }
}
