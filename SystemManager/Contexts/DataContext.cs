using Microsoft.EntityFrameworkCore;
using SystemManager.Models.Entities;

namespace SystemManager.Contexts
{
    internal class DataContext : DbContext
    {
        private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\rashe\OneDrive\Skrivbord\SystemManager\SystemManager\SystemManager\Contexts\db_SystemManager.mdf;Integrated Security=True;Connect Timeout=30";
        #region Constructor
        public DataContext()
        {
                
        }
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        #endregion

        #region Override
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        #region Entities
        public DbSet<CaseEntity> Cases { get; set; } = null!;
        public DbSet<CustomerEntity> Customers { get; set; } = null!;
        public DbSet<EmployeeEntity> Employees { get; set; } = null!;
        public DbSet<CommentEntity> Comments { get; set; } = null!;
        #endregion
    }
}
