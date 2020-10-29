using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
{
    public class InventoryContext : DbContext
    {
        protected InventoryContext()
        {
        }
        public InventoryContext(DbContextOptions<InventoryContext> options) : base(options)
        {
        }

        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }

#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-7HGH6SF;Database=Inventory;Trusted_Connection=True;");
            }
        }
    }
}
