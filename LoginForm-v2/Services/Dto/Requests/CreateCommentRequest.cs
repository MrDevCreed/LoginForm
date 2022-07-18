using LoginForm.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginForm.Services.Dto.Requests
{
    public class CreateCommentRequest
    {
        public CreateCommentRequest() { }
        public CreateCommentRequest(string title, string text,Account from)
        {
            this.Title = title;
            this.Text = text;
            this.From = from;
        }

        public string Title { get; set; }

        public string Text { get; set; }

        public Account From { get; set; }
    }
}
