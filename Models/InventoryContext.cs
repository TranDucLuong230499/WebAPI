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
        
        protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-7HGH6SF;Database=Inventory;Trusted_Connection=True;");
            }
        }
    }
}
