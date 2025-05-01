using System.Data.Entity;
using TechTalk.Models;
using TechTalk.ViewModels;

public class TechDbContext : DbContext
{
    public TechDbContext() : base("TechDbContext")
    {
    }

    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<ProductSubCategory> ProductSubCategories { get; set; }
    public DbSet<Program> Programs { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Define relationships
        modelBuilder.Entity<Product>()
            .HasRequired(p => p.ProductCategory)
            .WithMany(pc => pc.Products)
            .HasForeignKey(p => p.ProductCategoryId)
            .WillCascadeOnDelete(false);

        modelBuilder.Entity<ProductSubCategory>()
            .HasRequired(psc => psc.ProductCategory)
            .WithMany(pc => pc.ProductSubCategories)
            .HasForeignKey(psc => psc.ProductCategoryId)
            .WillCascadeOnDelete(false);

        modelBuilder.Entity<Product>()
            .HasRequired(p => p.ProductSubCategory)
            .WithMany(psc => psc.Products)
            .HasForeignKey(p => p.ProductSubCategoryId)
            .WillCascadeOnDelete(false);

        modelBuilder.Entity<Product>()
            .HasRequired(p => p.Program)
            .WithMany(prog => prog.Products)
            .HasForeignKey(p => p.ProgramId)
            .WillCascadeOnDelete(false);

        modelBuilder.Entity<OrderItem>()
            .HasRequired(oi => oi.Product)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(oi => oi.ProductId)
            .WillCascadeOnDelete(false);

        modelBuilder.Entity<Order>()
            .HasRequired(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId)
            .WillCascadeOnDelete(false);
    }
}
