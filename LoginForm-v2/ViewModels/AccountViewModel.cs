﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LoginForm.ViewModels
{
    public class AccountViewModel
    {
        public AccountViewModel() { }
        public AccountViewModel(string username, string password, string firstname, string lastname, string email, string phone)
        {
            this.UserName = username;
            this.Password = password;
            this.FirstName = firstname;
            this.LastName = lastname;
            this.Email = email;
            this.Phone = phone;
        }

        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [MinLength(2, ErrorMessage = "Field Must has 2 character")]
        [MaxLength(50, ErrorMessage = "Field characters Must Be Down of 50")]
        
        public string UserName { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [MinLength(5, ErrorMessage = "Field Must has 5 character")]
        [MaxLength(250, ErrorMessage = "Field characters Must Be Down of 250")]
        public string Password { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [MinLength(2, ErrorMessage = "Field Must has 2 character")]
        [MaxLength(20, ErrorMessage = "Field characters Must Be Down of 20")]

        public string FirstName { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        [MinLength(2, ErrorMessage = "Field Must has 2 character")]
        [MaxLength(20, ErrorMessage = "Field characters Must Be Down of 20")]
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";

        [Required(ErrorMessage = "This Field is Required")]
        [EmailAddress(ErrorMessage = "Plaase Enter a e-mail Address")]
        [Remote("IsEmailInUse", "Home")]
        public string Email { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        [RegularExpression(@"(\+98|0)?9\d{9}", ErrorMessage = "Please Enter Iranian Phone Number")]
        public string Phone { get; set; }
    }
}