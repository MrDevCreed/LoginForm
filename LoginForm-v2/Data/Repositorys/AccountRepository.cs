using LoginForm.Domain;
using System.Linq;
using System;

namespace LoginForm.Data.Repositorys
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRespository
    {
        private readonly Context context;

        public AccountRepository(Context context) : base(context)
        {
            this.context = context;
        }

        public bool ChangePassword(string UserName,string OldPassword, string NewPassword)
        {
            var Account = context.Accounts.Where(P => P.UserName == UserName && P.Password == OldPassword)
                                                .FirstOrDefault();

            if (Account != null)
            {
                Account.Password = NewPassword;
                return true;
            }

            return false;
        }

        public bool CheckEmailExists(string Email)
        {
            return context.Accounts.Where(P => P.Email == Email).Any();
        }
        public bool CheckPhoneExists(string Phone)
        {
            return context.Accounts.Where(P => P.Phone == Phone).Any();
        }

        public bool CheckUserNameAndPasswordExists(string UserName, string Password)
        {
            return context.Accounts.Where(P => P.UserName == UserName && P.Password == Password).Any();
        }

        public bool CheckUserNameExists(string UserName)
        {
            return context.Accounts.Where(P => P.UserName == UserName).Any();
        }

        public Account GetAccountByUserName(string UserName)
        {
            return context.Accounts.Where(P => P.UserName == UserName).First();
        }
    }

    public interface IAccountRespository : IRepositoryBase<Account>
    {
        bool CheckEmailExists(string Email);
        bool CheckPhoneExists(string Phone);
        bool CheckUserNameExists(string UserName);
        bool CheckUserNameAndPasswordExists(string UserName, string Password);
        bool ChangePassword(string UserName,string OldPassword,string NewPassword);
        Account GetAccountByUserName(string UserName);
    }
}
