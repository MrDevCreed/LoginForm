using LoginForm.Services.Dto.Responses;
using LoginForm.Services.Dto.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginForm.Data.Repositorys;
using LoginForm.ViewModels;

namespace LoginForm.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly IAccountRespository _accountRespository;
        public AccountsService(IAccountRespository accountRepository)
        {
            _accountRespository = accountRepository;
        }
        public CreateAccountResponse Create(CreateAccountRequest Account)
        {
            CreateAccountResponse createAccountResponse;
            if (_accountRespository.CheckEmailExists(Account.Email))
            {
                return createAccountResponse = new CreateAccountResponse()
                {
                    StatusCode = Dto.StatusCode.BadRequest,
                    Data = new AccountViewModel(Account.FirstName, Account.LastName, Account.Email, Account.Phone),
                    Message = "Email Used By Another Person"
                };
            }

            if (_accountRespository.CheckPhoneExists(Account.Phone))
            {
                return createAccountResponse = new CreateAccountResponse()
                {
                    StatusCode = Dto.StatusCode.BadRequest,
                    Data = new AccountViewModel(Account.FirstName, Account.LastName, Account.Email, Account.Phone),
                    Message = "Phone Used By Another Person"
                };
            }
            _accountRespository.Add(new Domain.Account(Account.FirstName,Account.LastName,Account.Email,Account.Phone));
            _accountRespository.SaveChanges();
            return createAccountResponse = new CreateAccountResponse()
            {
                StatusCode = Dto.StatusCode.Created,
                Data = new AccountViewModel(Account.FirstName, Account.LastName, Account.Email, Account.Phone),
                Message = "Account Created Succsesfull",
            };
        }
    }
    public interface IAccountsService
    {
        CreateAccountResponse Create(CreateAccountRequest Account);
    }
}
