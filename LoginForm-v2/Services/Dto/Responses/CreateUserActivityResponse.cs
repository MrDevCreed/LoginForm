using LoginForm.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginForm.Services.Dto.Responses
{
    public class CreateUserActivityResponse : ResponseBase
    {
        public UserActivityViewModel Data { get; set; }
    }
}
