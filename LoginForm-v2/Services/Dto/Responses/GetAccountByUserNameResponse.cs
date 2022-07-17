using LoginForm.Services.Dto;
using LoginForm.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginForm.Services.Dto.Responses
{
    public class GetAccountByUserNameResponse : ResponseBase
    {
        public AccountViewModel Data { get; set; }
    }
}
