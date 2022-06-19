using LoginForm.Data.Repositorys;
using LoginForm.Services.Dto.Requests;
using LoginForm.Services.Dto.Responses;
using LoginForm.Domain;
using LoginForm.ViewModels;
using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace LoginForm.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _CommentRepository;

        private readonly CommentConfiguration _CommentConfiguration;

        public CommentService(ICommentRepository commentRepository,IOptions<CommentConfiguration> options)
        {
            _CommentRepository = commentRepository;
            _CommentConfiguration = options.Value;
        }

        public CreateCommentResponse Create(CreateCommentRequest CreateCommentRequest)
        {
            try
            {
                _CommentRepository.Add(new Comment(CreateCommentRequest.Titel,
                                                   CreateCommentRequest.Text));
                _CommentRepository.SaveChanges();

                return new CreateCommentResponse()
                {
                    StatusCode = Dto.StatusCode.Created,
                    Message = "Comment Created Succesfully",
                    Data = new CommentViewModel(CreateCommentRequest.Titel,
                                                CreateCommentRequest.Text)
                };
            }
            catch
            {
                return new CreateCommentResponse()
                {
                    StatusCode = Dto.StatusCode.InternullServerError,
                    Message = "Internull Server Error Are happend",
                    Data = new CommentViewModel(CreateCommentRequest.Titel,
                                                CreateCommentRequest.Text)
                };
            }
        }

        public GetListCommentResponse GetList()
        {
            try
            {
                List<Comment> Comments = _CommentRepository.GetList(_CommentConfiguration.PageSize);
                List<CommentViewModel> CommentViewModels = new List<CommentViewModel>();

                foreach (var Item in Comments)
                {
                    CommentViewModels.Add(new CommentViewModel(Item.Titel,Item.Text));
                }

                return new GetListCommentResponse()
                {
                    StatusCode = Dto.StatusCode.Ok,
                    Message = "List Returned Sucsesfull",
                    Data = CommentViewModels,
                };
            }
            catch
            {
                return new GetListCommentResponse()
                {
                    StatusCode = Dto.StatusCode.InternullServerError,
                    Message = "Internull Server Error Are happend",
                    Data = null,
                };
            }
        }
    }

    public interface ICommentService
    {
        CreateCommentResponse Create(CreateCommentRequest CreateCommentRequest);

        GetListCommentResponse GetList();
    }
}
