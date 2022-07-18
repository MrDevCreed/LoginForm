using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LoginForm.Data.Repositorys
{
    public static class ClaimStore
    {
        public static List<Claim> Claims = new List<Claim>()
        {
            new Claim(ClaimTypeStore.UserName,true.ToString()),
            new Claim(ClaimTypeStore.Password,true.ToString())
        };
    }

    public static class ClaimTypeStore
    {
        public const string UserName = "UserName";
        public const string Password = "Password";
    }
}
