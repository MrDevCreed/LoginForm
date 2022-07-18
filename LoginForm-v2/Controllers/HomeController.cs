using Microsoft.AspNetCore.Mvc;
using LoginForm.ViewModels;
using LoginForm.Services;
using LoginForm.Services.Dto.Requests;
using LoginForm.Services.Dto.Responses;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Linq;
using LoginForm.ViewModels;
using LoginForm.Services.Dto.Requests;
using LoginForm.Services.Dto.Responses;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using System;
using LoginForm.Data.Repositorys;
using LoginForm.ActionFilters;
using LoginForm.Domain;

namespace LoginForm.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAccountsService _accountService;
        private readonly ICommentService _commentService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly CommentConfiguration _CommentConfiguration;
        private readonly SignInManager<IdentityUser> _SignInManager;

        public HomeController(ILogger<HomeController> logger,
                              IAccountsService accountService,
                              ICommentService commentService,
                              IOptions<CommentConfiguration> options,
                              UserManager<IdentityUser> userManager,
                              RoleManager<IdentityRole> roleManager,
                              SignInManager<IdentityUser> signInManager)
        {

            _logger = logger;
            _accountService = accountService;
            _commentService = commentService;
            _userManager = userManager;
            _roleManager = roleManager;
            _CommentConfiguration = options.Value;
            _SignInManager = signInManager;
        }

        public IActionResult Index()
        {
            _logger.Log(LogLevel.Information, "Page 'Index.cshtml' on HomeController Started...");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountViewModel Account)
        {
            _logger.LogDebug("Login Request Send.");
            var request = new CreateAccountRequest(Account.UserName,
                                                   Account.Password,
                                                   Account.FirstName,
                                                   Account.LastName,
                                                   Account.Email,
                                                   Account.Phone);

            _logger.LogDebug("Post on Data base Starting...");
            CreateAccountResponse createAccountResponse = _accountService.Create(request);
            _logger.LogDebug("Post on Database Seccessfull.");

            if (createAccountResponse.IsSuccses)
            {
                ViewBag.Message = createAccountResponse.Message;

                Response.Cookies.Append("User", $"{Account.UserName}~{Account.Password}", new CookieOptions()
                {
                    Expires = System.DateTimeOffset.Now.AddYears(5),
                });

                var User = new IdentityUser()
                {
                    UserName = Account.UserName,
                    Email = Account.Email,
                    EmailConfirmed = true,
                    PhoneNumber = Account.Phone,
                    PhoneNumberConfirmed = true,
                };

                var result = await _userManager.CreateAsync(User, Account.Password);

                List<Claim> Claims = new List<Claim>()
                {
                    new Claim("UserName",User.UserName),
                    new Claim("Password",Account.Password),
                };

                var res = await _userManager.AddClaimsAsync(User, Claims);

                await _SignInManager.SignInAsync(User, true);

                _logger.LogDebug($"Login Request Is Success");
                _logger.LogTrace($"FullName : {createAccountResponse.Data.FullName}\nEmail : {createAccountResponse.Data.Email}\nPhone : {createAccountResponse.Data.Phone}");

                return View(@"~/Views/Home/Index.cshtml", createAccountResponse.Data);
            }

            ViewBag.Message = createAccountResponse.Message;
            if (createAccountResponse.StatusCode == Services.Dto.StatusCode.InternullServerError)
                _logger.LogCritical("Ohhh , Connection To Database Field");

            _logger.LogInformation($"Login Request is Invalid");

            return View(@"~/Views/Home/Index.cshtml", createAccountResponse.Data);

        }

        [MyAuthorize(Policy: "UserName")]
        [HttpGet]
        public IActionResult ChangeAccountPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangeAccountPassword(ChangeAccountPasswordViewModel Account)
        {
            var Request = new ChangeAccountPasswordRequest(Account.UserName,
                                                           Account.OldPassword,
                                                           Account.NewPassword);

            ChangeAccountPasswordResponse ChangeAccountPasswordResponse = _accountService.ChangePassword(Request);

            if (ChangeAccountPasswordResponse.IsSuccses)
            {
                ViewBag.Message = ChangeAccountPasswordResponse.Message;

                Response.Cookies.Delete("User");
                Response.Cookies.Append("User", $"{Account.UserName}~{Account.NewPassword}", new CookieOptions()
                {
                    Expires = System.DateTimeOffset.Now.AddYears(5),
                });

                return View(Account);
            }

            ViewBag.Message = ChangeAccountPasswordResponse.Message;

            return View(Account);
        }

        public IActionResult Comment()
        {
            return View(new CreateCommentInputViewModel(_commentService.GetList(
                                                            _CommentConfiguration.PageSize, 1).Data));
        }

        //[Log]
        public IActionResult CreateComment(CommentViewModel Comment)
        {
            GetAccountByUserNameResponse Response = _accountService.GetAccountByUserName(
                                                                        new GetAccountByUserNameRequest(
                                                                            User.Claims.Where(P =>
                                                                            P.Type == "UserName")
                                                                            .First().Value));

            var CreateCommentRequest = new CreateCommentRequest(Comment.Title,
                                                                Comment.Text,
                                                                Comment.From);

            CreateCommentResponse CreateCommentResponse = _commentService.Create(CreateCommentRequest,
                                                                                 Response.Data.UserName);

            if (CreateCommentResponse.IsSuccses)
            {
                ViewBag.Message = CreateCommentResponse.Message;
                return RedirectToAction("Comment");
            }

            ViewBag.Message = CreateCommentResponse.Message;
            return RedirectToAction("Comment");
        }

        public async Task<IActionResult> ExitAccount()
        {
            Response.Cookies.Delete("User");

            await _SignInManager.SignOutAsync();

            return View(@"~/Views/Home/Index.cshtml");
        }

        public async Task<IActionResult> IsEmailInUse(string Email)
        {
            var User = await _userManager.FindByEmailAsync(Email);

            if (User == null)
            {
                return Json(true);
            }

            return Json("Email is Already Taken");
        }
    }
}
