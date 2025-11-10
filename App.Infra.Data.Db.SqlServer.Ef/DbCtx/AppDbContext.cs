using App.Domain.Core.CategoryAgg.Entities;
using App.Domain.Core.TaskAgg.Entities;
using App.Domain.Core.UserAgg.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Infra.Data.Db.SqlServer.Ef.DbCtx
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            modelBuilder.Entity<MyTask>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Category>()
                .HasKey(c => c.Id);
            
            modelBuilder.Entity<User>()
                .HasKey(c => c.Id);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MyTask> Tasks { get; set; }
    }
}
