using LoginForm.Services.Dto.Responses;
using LoginForm.Services.Dto.Requests;
using System;
using LoginForm.Data.Repositorys;
using LoginForm.ViewModels;
using LoginForm.Services.Dto.Responses;
using LoginForm.Services.Dto.Requests;
using LoginForm.ViewModels;
using LoginForm.Domain;

namespace LoginForm.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly IAccountRespository _accountRespository;
        public AccountsService(IAccountRespository accountRepository)
        {
            _accountRespository = accountRepository;
        }

        public ChangeAccountPasswordResponse ChangePassword(ChangeAccountPasswordRequest Request)
        {
            try
            {
                string UserName = Request.UserName;
                string OldPassword = Request.OldPassword;
                string NewPassword = Request.NewPassword;

                _accountRespository.ChangePassword(UserName, OldPassword, NewPassword);
                _accountRespository.SaveChanges();

                return new ChangeAccountPasswordResponse()
                {
                    StatusCode = Dto.StatusCode.Ok,
                    Message = "Password Changed Succesfull",
                    Data = new ChangeAccountPasswordViewModel(UserName, OldPassword, NewPassword)
                };
            }
            catch (Exception ex) when (ex is ArgumentException || ex is ArgumentNullException)
            {
                return new ChangeAccountPasswordResponse()
                {
                    StatusCode = Dto.StatusCode.BadRequest,
                    Message = "Invalid Parameter Passed",
                };
            }
            catch
            {
                return new ChangeAccountPasswordResponse()
                {
                    StatusCode = Dto.StatusCode.InternullServerError,
                    Message = "Internal Server Error Are happendS",
                };
            }
        }

        public CreateAccountResponse Create(CreateAccountRequest Account)
        {
            try
            {
                string UserName = Account.UserName;
                string Password = Account.Password;
                string FirstName = Account.FirstName;
                string LastName = Account.LastName;
                string Email = Account.Email;
                string Phone = Account.Phone;

                if (_accountRespository.CheckUserNameExists(Account.UserName))
                {
                    return new CreateAccountResponse()
                    {
                        StatusCode = Dto.StatusCode.BadRequest,
                        Data = new AccountViewModel(UserName, Password, FirstName, LastName, Email, Phone),
                        Message = "UserName Used By Another Person"
                    };
                }

                if (_accountRespository.CheckEmailExists(Account.Email))
                {
                    return new CreateAccountResponse()
                    {
                        StatusCode = Dto.StatusCode.BadRequest,
                        Data = new AccountViewModel(UserName, Password, FirstName, LastName, Email, Phone),
                        Message = "Email Used By Another Person"
                    };
                }

                if (_accountRespository.CheckPhoneExists(Phone))
                {
                    return new CreateAccountResponse()
                    {
                        StatusCode = Dto.StatusCode.BadRequest,
                        Data = new AccountViewModel(UserName, Password, FirstName, LastName, Email, Phone),
                        Message = "Phone Used By Another Person"
                    };
                }

                _accountRespository.Add(new Domain.Account(UserName, Password, FirstName, LastName, Email, Phone));
                _accountRespository.SaveChanges();

                return new CreateAccountResponse()
                {
                    StatusCode = Dto.StatusCode.Created,
                    Data = new AccountViewModel(UserName, Password, FirstName, LastName, Email, Phone),
                    Message = "Account Created Succsesfull",
                };
            }
            catch (Exception ex) when (ex is ArgumentException || ex is ArgumentNullException)
            {
                return new CreateAccountResponse()
                {
                    StatusCode = Dto.StatusCode.BadRequest,
                    Message = ex.Message,
                };
            }
            catch
            {
                return new CreateAccountResponse()
                {
                    StatusCode = Dto.StatusCode.InternullServerError,
                    Message = "Internull Server Error Are happend",
                };
            }
        }

        public GetAccountByUserNameResponse GetAccountByUserName(GetAccountByUserNameRequest Request)
        {
            try
            {
                 Account account = _accountRespository.GetAccountByUserName(Request.UserName);
          
                return new GetAccountByUserNameResponse()
                {
                    StatusCode = Dto.StatusCode.Ok,
                    Message = "Password Changed Succesfull",
                    Data = new AccountViewModel(account.UserName,
                                                account.Password,
                                                account.FirstName,
                                                account.LastName,
                                                account.Email,
                                                account.Phone)
                    {
                        Id = account.Id,
                    },
                };
            }
            catch
            {
                return new GetAccountByUserNameResponse()
                {
                    StatusCode = Dto.StatusCode.BadRequest,
                    Message = "Internal Server Error Are happend",
                };
            }
        }
    }
    public interface IAccountsService
    {
        CreateAccountResponse Create(CreateAccountRequest Account);
        ChangeAccountPasswordResponse ChangePassword(ChangeAccountPasswordRequest Request);
        GetAccountByUserNameResponse GetAccountByUserName(GetAccountByUserNameRequest Request);
    }
}
