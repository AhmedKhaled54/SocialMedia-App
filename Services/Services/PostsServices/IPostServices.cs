using Data.Entity;
using Data.Entity.Posts;
using Infrastructure.Specification.PostSpecifications;
using Microsoft.AspNetCore.Http;
using MimeKit.Tnef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.PostsServices
{
    public  interface IPostServices
    {
        Task<Post>CreatePost(int userid ,string?caption,List<PostMedia>?media );
        Task<List<PostMedia>> PostMediaProcess (List<IFormFile> media);

        IQueryable<Post> GetFeedPost(int userid );
        Task<IEnumerable<Post>> GetFeedPostList(int userid);

        IQueryable<Post> getPostSepecification(PostSpecification post);

        Task<Post> GetPostById(int PostId);
        Task<bool> UpdatePost(Post post);
        Task<bool> DeletePost(Post post);
        Task<bool>DeletePostMedia (List<PostMedia >media);
      
    }
}
