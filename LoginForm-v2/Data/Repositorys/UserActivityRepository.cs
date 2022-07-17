using LoginForm.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginForm.Data.Repositorys
{
    public class UserActivityRepository : RepositoryBase<UserActivity> , IUserActivityRepository
    {
        private readonly Context _context;

        public UserActivityRepository(Context context) : base(context)
        {
            this._context = context;
        }
    }

    public interface IUserActivityRepository : IRepositoryBase<UserActivity>
    {

    }
}
