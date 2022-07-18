using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginForm.Data.Repositorys
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T:class
    {
        private readonly Context _context;

        protected DbSet<T> _dbSet;

        public RepositoryBase(Context context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public void Add(T DbSet)
        {
            _dbSet.Add(DbSet);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }

    public interface IRepositoryBase<T>
    {
        void SaveChanges();

        void Add(T DbSet);
    }
}
