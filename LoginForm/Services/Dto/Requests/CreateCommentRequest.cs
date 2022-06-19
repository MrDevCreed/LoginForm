using LoginForm.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginForm.Services.Dto.Requests
{
    public class CreateCommentRequest
    {
        public CreateCommentRequest(string titel, string text)
        {
            this.Titel = titel;
            this.Text = text;
        }

        public string Titel { get; set; }

        public string Text { get; set; }
    }
}
