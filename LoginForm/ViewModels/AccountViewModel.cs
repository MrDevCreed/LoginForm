using System.ComponentModel.DataAnnotations;

namespace LoginForm.ViewModels
{
    public class AccountViewModel
    {
        public AccountViewModel() { }
        public AccountViewModel(string firstname, string lastname, string email, string phone)
        {
            this.FirstName = firstname;
            this.LastName = lastname;
            this.Email = email;
            this.Phone = phone;
        }

        [Required(ErrorMessage = "This Field is Required")]
        [MinLength(2, ErrorMessage = "Field Must has 2 character")]
        [MaxLength(20, ErrorMessage = "Field characters Must Be Down of 20")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [MinLength(2, ErrorMessage = "Field Must has 2 character")]
        [MaxLength(20, ErrorMessage = "Field characters Must Be Down of 20")]
        public string LastName { get; set; }

        public string FullName => FirstName + " " + LastName;

        [Required(ErrorMessage = "This Field is Required")]
        [EmailAddress(ErrorMessage = "Plaase Enter a e-mail Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [RegularExpression(@"(\+98|0)?9\d{9}", ErrorMessage = "Please Enter Iranian Phone Number")]
        public string Phone { get; set; }
    }
}
