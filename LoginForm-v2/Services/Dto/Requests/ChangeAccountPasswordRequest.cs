using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace LoginForm.Services.Dto.Requests
{
    public class ChangeAccountPasswordRequest
    {
        public ChangeAccountPasswordRequest() { }
        public ChangeAccountPasswordRequest(string username, string oldPassword,string newPassword)
        {
            this.UserName = username;
            this.OldPassword = oldPassword;
            this.NewPassword = newPassword;
        }

        [Required(ErrorMessage = "This Field is Required")]
        [MinLength(2, ErrorMessage = "Field Must has 2 character")]
        [MaxLength(50, ErrorMessage = "Field characters Must Be Down of 50")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [MinLength(5, ErrorMessage = "Field Must has 5 character")]
        [MaxLength(250, ErrorMessage = "Field characters Must Be Down of 250")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [MinLength(5, ErrorMessage = "Field Must has 5 character")]
        [MaxLength(250, ErrorMessage = "Field characters Must Be Down of 250")]
        public string NewPassword { get; set; }
    }
}
