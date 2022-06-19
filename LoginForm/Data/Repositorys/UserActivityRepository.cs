using LoginForm.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginForm.Data.Repositorys
{
    public class UserActivityRepository : IUserActivityRepository
    {
        private readonly Context _context;

        public UserActivityRepository(Context context)
        {
            this._context = context;
        }

        public void Add(UserActivity UserActivity)
        {
            _context.UserActivities.Add(UserActivity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }

    public interface IUserActivityRepository
    {
        void Add(UserActivity UserActivity);
        void SaveChanges();
    }
}
