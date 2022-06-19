using LoginForm.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginForm.Services.Dto.Responses
{
    public class CreateCommentResponse : ResponseBase
    {
        public CommentViewModel Data { get; set; }
    }
}
