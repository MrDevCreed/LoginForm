using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LoginForm.Domain;
using LoginForm.Data.Repositorys;
using LoginForm.ViewModels;
using LoginForm.Models;
using LoginForm.Services;
using LoginForm.Services.Dto.Requests;
using LoginForm.Services.Dto.Responses;
using LoginForm.ActionFilters;

namespace LoginForm.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAccountsService _accountService;

        private readonly ICommentService _commentService;
        public HomeController(IAccountsService accountService,ICommentService commentService)
        {
            _accountService = accountService;
            _commentService = commentService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AccountViewModel Account)
        {
            var request = new CreateAccountRequest(Account.FirstName,
                                                   Account.LastName,
                                                   Account.Email,
                                                   Account.Phone);

            CreateAccountResponse createAccountResponse = _accountService.Create(request);

            if (createAccountResponse.IsSuccses)
            {
                ViewBag.Message = createAccountResponse.Message;
                return View(@"~/Views/Home/Index.cshtml", createAccountResponse.Data);
            }

            ViewBag.Message = createAccountResponse.Message;

            return View(@"~/Views/Home/Index.cshtml", createAccountResponse.Data);

        }

        public IActionResult Comment()
        {
            return View(new CreateCommentInputViewModel(_commentService.GetList().Data));
        }

        //[Log]
        public IActionResult CreateComment(CommentViewModel Comment)
        {
            var CreateCommentRequest = new CreateCommentRequest(Comment.Titel,
                                                                Comment.Text);

            CreateCommentResponse CreateCommentResponse = _commentService.Create(CreateCommentRequest);

            if (CreateCommentResponse.IsSuccses)
            {
                ViewBag.Message = CreateCommentResponse.Message;
                return View(@"~/Views/Home/Comment.cshtml", new CreateCommentInputViewModel(
                                                                _commentService.GetList().Data));
            }

            ViewBag.Message = CreateCommentResponse.Message;
            return View(@"~/Views/Home/Comment.cshtml", new CreateCommentInputViewModel(
                                                            _commentService.GetList().Data));
        }
    }
}
