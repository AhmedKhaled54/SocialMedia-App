using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Core.Bases;
using Core.Feature.Comments.Query.Models;
using Core.Feature.Comments.Query.Results;
using Core.Wrappers;
using Infrastructure.Specification.CommentsSpecification;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Services.Services.CommentsServices;
using Services.Services.RactsServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Comments.Query.Handler
{
    public class AddCommentQueryHandler : ResponseHanlder,
        IRequestHandler<GetReactCommentDetailsQuery, Response<GetReactCommentDetailsQueryResult>>,
        IRequestHandler<GetCommentsQuery, Response<Pagination<GetCommentsQueryResult>>>
    {
        #region Feild
        private readonly IReactServices _reactServices;
        private readonly ICommentServices _commentServices;
        private readonly IMapper _mapper;
        private readonly IConfiguration _confiq;

        #endregion
        #region Ctor 
        public AddCommentQueryHandler(IReactServices reactServices, IMapper mapper, ICommentServices commentServices, IConfiguration confiq)
        {
            _reactServices = reactServices;
            _mapper = mapper;
            _commentServices = commentServices;
            _confiq = confiq;
        }
        #endregion
        public async Task<Response<GetReactCommentDetailsQueryResult>> Handle(GetReactCommentDetailsQuery request, CancellationToken cancellationToken)
        {
            var comment = await _reactServices.GetCommentDetails(request.CommentId);
            if (comment == null)
                return NotFound<GetReactCommentDetailsQueryResult>("No Reacts in This Comment ");
            var result = _mapper.Map<GetReactCommentDetailsQueryResult>(comment);
            return Success(result);

        }

        public async Task<Response<Pagination<GetCommentsQueryResult>>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
        {

            var comments = _commentServices.GetCommentsSepcs(request.PostId);
            // var pagination =await  comments.ToPaginationListAsync(request.PageNumber, request.PageSize);
            var result = await _mapper.ProjectTo<GetCommentsQueryResult>(comments)
                .ToPaginationListAsync(request.PageNumber, request.PageSize);
            foreach (var item in result.Data)
            {
                var dict = comments.First(c => c.Id == item.CommentId);
                item.ReactsCount = dict.Likes.GroupBy(c => c.LikeType).ToDictionary(c => c.Key, c => c.Count());
                item.ReactDetails = dict.Likes.GroupBy(c => c.LikeType)
                    .ToDictionary(c => c.Key, c => c.Select(s => new GetCommentUserQueryResult
                    {
                        UserId = s.UsertId,
                        UserName = s.User.UserName ?? "UnKown "
                    }).ToList());

                if (item.Medias.Any()&&item.Medias!=null)
                {
                    foreach (var Url in item.Medias)
                    {
                        if (!string.IsNullOrEmpty(Url.Url))
                            Url.Url = _confiq["BaseUrl"] + Url.Url;
                    }
                }
            }


            return Success(result);

        }
    }
}
