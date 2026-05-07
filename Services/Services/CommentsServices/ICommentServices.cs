using Data.Entity.Comments;
using Infrastructure.Specification.CommentsSpecification;
using Microsoft.AspNetCore.Http;
using MimeKit.Tnef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.CommentsServices
{
    public interface ICommentServices
    {
        Task<Comment> AddComment(int UserId, int PostId, string? Content, List<CommentMedia>? medias);
        Task<List<CommentMedia>> CommentmedaiProcess(List<IFormFile> files);
        IQueryable<Comment> GetAllComments(int PostId);
        IQueryable<Comment> GetCommentsSepcs(int PostId);

        Task<bool> DeleteCommentMedia(List<CommentMedia> commentMedias);
        Task<bool> DeleteComment(Comment comment);
        Task<bool> UpdateComment(Comment comment);
        Task<Comment> GetCommentById(int CommentId);
    }
}
