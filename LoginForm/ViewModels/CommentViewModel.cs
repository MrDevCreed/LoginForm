using LoginForm.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoginForm.ViewModels
{
    public class CommentViewModel
    {
        public CommentViewModel() { }
        public CommentViewModel(string titel, string text)
        {
            this.Titel = titel;
            this.Text = text;
        }

        [Required]
        public string Titel { get; set; }

        [Required]
        [MinLength(3,ErrorMessage = "Character of Text Must Between [3-500]")]
        [MaxLength(500, ErrorMessage = "Character of Text Must Between [3-500]")]
        public string Text { get; set; }
    }
}
