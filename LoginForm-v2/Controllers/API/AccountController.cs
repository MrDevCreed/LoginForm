using LoginForm.Services;
using LoginForm.Services.Dto.Requests;
using LoginForm.Services.Dto.Responses;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LoginForm.Controllers.API
{
    public class AccountController : Controller
    {
        private readonly IAccountsService _accountsService;
        public AccountController(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }

        public IActionResult CreateAccount(CreateAccountRequest CreateAccountRequest)
        {
            var request = new CreateAccountRequest(CreateAccountRequest.UserName,
                                                   CreateAccountRequest.Password,
                                                   CreateAccountRequest.FirstName,
                                                   CreateAccountRequest.LastName,
                                                   CreateAccountRequest.Email,
                                                   CreateAccountRequest.Phone);

            CreateAccountResponse createAccountResponse = _accountsService.Create(request);

            if (createAccountResponse.IsSuccses)
            {
                return Json($"Message : {createAccountResponse.Message} Status Code : {createAccountResponse.StatusCode}");
            }

            return Json($"Message : {createAccountResponse.Message} Status Code : {createAccountResponse.StatusCode}");
        }
    }
}
