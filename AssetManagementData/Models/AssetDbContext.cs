using Microsoft.EntityFrameworkCore;

namespace AssetManagementData
{
    public class AssetDbContext : DbContext
    {
        public AssetDbContext(DbContextOptions<AssetDbContext> options)
            : base(options) { }
        public DbSet<AssetModel> Assets { get; set; }
    }
}
