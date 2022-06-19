using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginForm.ViewModels;

namespace LoginForm.Services.Dto.Responses
{
    public class GetListCommentResponse : ResponseBase
    {
        public List<CommentViewModel> Data { get; set; }
    }
}
