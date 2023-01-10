using Microsoft.EntityFrameworkCore;
using ProjectApi.Models;

namespace SQL_CRUD_X_HashiCorp_Vault.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Project> Projects { get; set; }
    }
}
