using Data.Entity;
using Data.Entity.Comments;
using Data.Enums.Media;
using Infrastructure.Abstract;
using Infrastructure.Specification.CommentsSpecification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using MimeKit.Cryptography;
using Services.FilesServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.CommentsServices
{
    public class CommentServices : ICommentServices
    {
        private readonly IUnitOfWork _UOW;
        private readonly IFileServices _fileServices;
        public CommentServices(IUnitOfWork uOW, IFileServices fileServices)
        {
            _UOW = uOW;
            _fileServices = fileServices;
        }
        public async Task<Comment> AddComment(int UserId, int PostId, string? Content, List<CommentMedia>? medias)
        {
            var comment = new Comment
            {
                UserId = UserId,
                PostId = PostId,
                Content = Content
            };
            if (medias != null && medias.Any())
                comment.Media = medias;

            await _UOW.Repository<Comment>().AddAsync(comment);
            await _UOW.Complete();
            return comment;
        }

        public async Task<List<CommentMedia>> CommentmedaiProcess(List<IFormFile> files)
        {
            var result = new List<CommentMedia>();

            foreach (var item in files)
            {

                var path = Path.GetExtension(item.FileName.ToLower());
                var type = path == ".mp4" ? MedaiType.Video : MedaiType.Image;
                var folder = path == ".mp4" ? "Comments/Videos" : "Comments/Images";
                var Url = await _fileServices.UploadImage(item, folder);


                result.Add(new CommentMedia
                {
                    Type = type,
                    CreatedAt = DateTime.Now,
                    Url = Url
                });
            }

            return result;

        }



        //before specification 
        public IQueryable<Comment> GetAllComments(int PostId)
        {
            var comments = _UOW.Repository<Comment>()
                .GetAllpridicated(c => c.PostId == PostId, new[] { "Likes.User", "Likes" });
            return comments;

        }
        //after specification
        public IQueryable<Comment> GetCommentsSepcs(int PostId)
        {
            var comment = new CommentWithLikeandUserlikeSpecification(PostId);
            var comments = _UOW.Repository<Comment>().GetEntitiesWithSpecs(comment);
            return comments;
        }

        public async Task<bool> UpdateComment(Comment comment)
        {
            _UOW.Repository<Comment>().Update(comment);
            await _UOW.Complete();
            return true;
        }
        public async Task<bool> DeleteComment(Comment comment)
        {
            var trans = await _UOW.BeginTransactionAsync();
            try
            {
                var likes = _UOW.Repository<Like>().GetAllpridicated(l => l.CommentId == comment.Id).ToList();
                _UOW.Repository<Comment>().Delete(comment);
                _UOW.Repository<Like>().RemoveRange(likes);
                await _UOW.Complete();
                await trans.CommitAsync();
                return true;

            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                throw new Exception(ex.Message);
            }

        }

        public async Task<bool> DeleteCommentMedia(List<CommentMedia> commentMedias)
        {
            _UOW.Repository<CommentMedia>().RemoveRange(commentMedias);
            await _UOW.Complete();
            return true;
        }

        public async Task<Comment> GetCommentById(int CommentId)
        {
            var specs = new CommetWithmediaSepecification(CommentId);
            var comment = await _UOW.Repository<Comment>().GetEntityByIdSepcs(specs);
            return comment;
        }
    }
}
