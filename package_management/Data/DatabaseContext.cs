using Microsoft.EntityFrameworkCore;

namespace package_management.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Package> Packages { get; set; }
        public DbSet<RecipientContactModel> RecipientContactModel { get; set; }
        public DbSet<InfoPackageModel> InfoPackageModel { get; set; }
    }
}
