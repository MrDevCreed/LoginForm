using LoginForm.Data.Repositorys;
using LoginForm.Services.Dto.Requests;
using LoginForm.Services.Dto.Responses;
using LoginForm.Domain;
using LoginForm.ViewModels;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using System;

namespace LoginForm.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _CommentRepository;
        private readonly IAccountRespository _accountRespository;

        public CommentService(ICommentRepository commentRepository,IAccountRespository accountRespository)
        {
            _CommentRepository = commentRepository;
            _accountRespository = accountRespository;
        }

        public CreateCommentResponse Create(CreateCommentRequest CreateCommentRequest,string UserName)
        {
            try
            {
                Comment comment = new Comment(CreateCommentRequest.Title,
                                              CreateCommentRequest.Text,
                                              _accountRespository.GetAccountByUserName(UserName));

                _CommentRepository.Add(comment);

                _CommentRepository.SaveChanges();

                return new CreateCommentResponse()
                {
                    StatusCode = Dto.StatusCode.Created,
                    Message = "Comment Created Succesfully",
                    Data = new CommentViewModel(comment.Title,
                                                comment.Text,
                                                comment.CreatedAt,
                                                comment.From)
                };
            }
            catch (Exception ex) when (ex is ArgumentException || ex is ArgumentNullException)
            {
                return new CreateCommentResponse()
                {
                    StatusCode = Dto.StatusCode.BadRequest,
                    Message = ex.Message,
                };
            }
            catch
            {
                return new CreateCommentResponse()
                {
                    StatusCode = Dto.StatusCode.InternullServerError,
                    Message = "Internull Server Error Are happend",
                };
            }
        }

        public GetListCommentResponse GetList(int PageSize,int PageNumber)
        {
            try
            {
                if (PageSize == null || PageSize < 1 || PageNumber == null || PageNumber < 1)
                {
                    throw new ArgumentException("Invalid Parameter Passed");
                }

                List<Comment> Comments = _CommentRepository.GetList(PageSize, PageNumber);
                List<CommentViewModel> CommentViewModels = new List<CommentViewModel>();

                foreach (var Item in Comments)
                {
                    CommentViewModels.Add(new CommentViewModel(Item.Title, Item.Text, Item.CreatedAt,Item.From));
                }

                return new GetListCommentResponse()
                {
                    StatusCode = Dto.StatusCode.Ok,
                    Message = "List Returned Sucsesfull",
                    Data = CommentViewModels,
                };
            }
            catch (ArgumentException ex)
            {
                return new GetListCommentResponse()
                {
                    StatusCode = Dto.StatusCode.BadRequest,
                    Message = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new GetListCommentResponse()
                {
                    StatusCode = Dto.StatusCode.InternullServerError,
                    Message = "Internull Server Error Are happend"
                };
            }
        }
    }

    public interface ICommentService
    {
        CreateCommentResponse Create(CreateCommentRequest CreateCommentRequest,string UserName);

        GetListCommentResponse GetList(int PageSize,int PagrNumber);
    }
}
