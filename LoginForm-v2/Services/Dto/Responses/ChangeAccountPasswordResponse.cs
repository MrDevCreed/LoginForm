using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginForm.Services.Dto;
using LoginForm.ViewModels;

namespace LoginForm.Services.Dto.Responses
{
    public class ChangeAccountPasswordResponse : ResponseBase
    {
        public ChangeAccountPasswordViewModel Data { get; set; }
    }
}
