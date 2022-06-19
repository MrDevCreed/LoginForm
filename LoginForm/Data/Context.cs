using Microsoft.EntityFrameworkCore;
using LoginForm.Domain;

namespace LoginForm.Data
{
    public class Context : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<UserActivity> UserActivities { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public Context(DbContextOptions<Context> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseSqlServer("Data Source=(localdb)\v11.0;Integrated Security=True;Initial Catalog=LoginFormASPNET");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Account>().HasIndex(P => P.Email).IsUnique();
            modelBuilder.Entity<Account>().HasIndex(P => P.Phone).IsUnique();
        }

    }
}
