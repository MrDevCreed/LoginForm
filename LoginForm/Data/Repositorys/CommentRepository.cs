using LoginForm.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginForm.Data.Repositorys
{
    public class CommentRepository : ICommentRepository
    {
        private readonly Context _Context;
        public CommentRepository(Context context)
        {
            _Context = context;
        }

        public void Add(Comment Comment)
        {
            _Context.Comments.Add(Comment);
        }

        public List<Comment> GetList(int PageSize)
        {
            return _Context.Comments.Skip(Math.Max(0,_Context.Comments.Count() - PageSize)).ToList();
        }

        public void SaveChanges()
        {
            _Context.SaveChanges();
        }
    }

    public interface ICommentRepository
    {
        void Add(Comment Comment);

        List<Comment> GetList(int PageSize);

        void SaveChanges();
    }
}
