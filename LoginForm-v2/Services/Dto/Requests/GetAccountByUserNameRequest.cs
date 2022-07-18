using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoginForm.Services.Dto.Requests
{
    public class GetAccountByUserNameRequest
    {
        public GetAccountByUserNameRequest() { }
        public GetAccountByUserNameRequest(string username)
        {
            this.UserName = username;
        }

        [Required(ErrorMessage = "This Field is Required")]
        [MinLength(2, ErrorMessage = "Field Must has 2 character")]
        [MaxLength(50, ErrorMessage = "Field characters Must Be Down of 50")]
        public string UserName { get; set; }

    }
}
