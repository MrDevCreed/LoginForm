using LoginForm.Domain;
using System.Linq;

namespace LoginForm.Data.Repositorys
{
    public class AccountRepository : IAccountRespository
    {
        private readonly Context context;

        public AccountRepository(Context context)
        {
            this.context = context;
        }

        public void Add(Account account)
        {
            context.Accounts.Add(account);
        }
        public bool CheckEmailExists(string Email)
        {
            if (context.Accounts.Where(P => P.Email == Email).Any())
            {
                return true;
            }
            return false;
        }
        public bool CheckPhoneExists(string Phone)
        {
            if (context.Accounts.Where(P => P.Phone == Phone).Any())
            {
                return true;
            }
            return false;
        }
        public void SaveChanges()
        {
            context.SaveChanges();
        }

    }

    public interface IAccountRespository
    {
        void Add(Account account);
        void SaveChanges();
        bool CheckEmailExists(string Email);
        bool CheckPhoneExists(string Phone);
    }
}
