using LoginForm.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginForm.Data.Repositorys
{
    public class CommentRepository : RepositoryBase<Comment>, ICommentRepository
    {
        private readonly DbSet<Comment> _dbSet;
        public CommentRepository(Context context) : base(context)
        {
            _dbSet = context.Comments;
        }

        public List<Comment> GetList(int PageSize,int PageNumber)
        {
            return _dbSet.OrderByDescending(P => P.CreatedAt).Skip(PageNumber * PageSize - PageSize).Include(P => P.From).Take(PageSize).ToList();
        }
    }

    public interface ICommentRepository : IRepositoryBase<Comment>
    {
        List<Comment> GetList(int PageSize,int PageNumber);
    }
}
