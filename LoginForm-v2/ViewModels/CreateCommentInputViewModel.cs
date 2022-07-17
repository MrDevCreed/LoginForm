using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginForm.ViewModels
{
    public class CreateCommentInputViewModel
    {
        public CreateCommentInputViewModel(List<CommentViewModel> commentViewModels)
        {
            this.CommentViewModels = commentViewModels;
        }

        public List<CommentViewModel> CommentViewModels { get; set; }

        public CommentViewModel Comment { get; set; }
    }
}
