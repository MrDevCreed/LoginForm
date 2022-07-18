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
        public CommentViewModel(string title, string text,DateTime createdAt,Account from)
        {
            this.Title = title;
            this.Text = text;
            this.From = from;
            this.CreatedAt = createdAt;
        }

        [Required]
        public string Title { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Character of Text Must Between [3-500]")]
        [MaxLength(500, ErrorMessage = "Character of Text Must Between [3-500]")]
        public string Text { get; set; }

        public Account From { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
