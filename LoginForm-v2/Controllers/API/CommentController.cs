
using LoginForm.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginForm;
using LoginForm.Services.Dto.Requests;
using LoginForm.Services.Dto.Responses;
using Microsoft.Extensions.Logging;
using LoginForm.Services;

namespace LoginForm.Controllers.API
{
    public class CommentController : Controller
    {
        private readonly ILogger<CommentController> _logger;

        private readonly ICommentService _commentService;

        public CommentController(ILogger<CommentController> logger,
                              ICommentService commentService)
        {
            _logger = logger;
            _commentService = commentService;
        }

        [HttpGet]
        public IActionResult Index(int PageSize, int PageNumber)
        {
            GetListCommentResponse Create = _commentService.GetList(PageSize, PageNumber);

            string Comments = "";
            foreach (var item in Create.Data)
            {
                Comments += $"Title:{item.Title}Text:{item.Text}CreatedAt:{item.CreatedAt}";
            }

            return Json($"Message : {Create.Message} Status Code : {Create.StatusCode} {Comments}");
        }

        //[HttpPost]
        //public IActionResult CreateComment(CreateCommentRequest Comment)
        //{
        //    var CreateCommentRequest = new CreateCommentRequest(Comment.Title,
        //                                                        Comment.Text,
        //                                                        Comment.From);

        //    CreateCommentResponse CreateCommentResponse = _commentService.Create(CreateCommentRequest);

        //    if (CreateCommentResponse.IsSuccses)
        //    {
        //        return Json($"Message : {CreateCommentResponse.Message} Status Code : {CreateCommentResponse.StatusCode}");
        //    }

        //    return Json($"Message : {CreateCommentResponse.Message} Status Code : {CreateCommentResponse.StatusCode}");
        //}
    }
}
