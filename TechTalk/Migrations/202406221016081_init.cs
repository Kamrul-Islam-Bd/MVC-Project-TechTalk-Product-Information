namespace TechTalk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        CartItemId = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        Product_ProductId = c.Int(nullable: false),
                        Cart_CartId = c.Int(),
                    })
                .PrimaryKey(t => t.CartItemId)
                .ForeignKey("dbo.Products", t => t.Product_ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Carts", t => t.Cart_CartId)
                .Index(t => t.Product_ProductId)
                .Index(t => t.Cart_CartId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductName = c.String(nullable: false, maxLength: 200),
                        ProductDescription = c.String(nullable: false, maxLength: 1000),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StockQuantity = c.Int(nullable: false),
                        PictureUrl = c.String(),
                        ProgramId = c.Int(nullable: false),
                        ProductCategoryId = c.Int(nullable: false),
                        ProductSubCategoryId = c.Int(nullable: false),
                        DiscountId = c.Int(),
                        ViewCount = c.Int(nullable: false),
                        CartAddCount = c.Int(nullable: false),
                        AverageRating = c.Double(nullable: false),
                        TotalRatings = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Discounts", t => t.DiscountId)
                .ForeignKey("dbo.ProductCategories", t => t.ProductCategoryId)
                .ForeignKey("dbo.ProductSubCategories", t => t.ProductSubCategoryId)
                .ForeignKey("dbo.Programs", t => t.ProgramId)
                .Index(t => t.ProgramId)
                .Index(t => t.ProductCategoryId)
                .Index(t => t.ProductSubCategoryId)
                .Index(t => t.DiscountId);
            
            CreateTable(
                "dbo.Discounts",
                c => new
                    {
                        DiscountId = c.Int(nullable: false, identity: true),
                        DiscountPercentage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DiscountId)
                .ForeignKey("dbo.ProductCategories", t => t.ProductCategoryId, cascadeDelete: true)
                .Index(t => t.ProductCategoryId);
            
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        ProductCategoryId = c.Int(nullable: false, identity: true),
                        ProCatName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ProductCategoryId);
            
            CreateTable(
                "dbo.ProductSubCategories",
                c => new
                    {
                        ProductSubCategoryId = c.Int(nullable: false, identity: true),
                        ProSubCatName = c.String(nullable: false, maxLength: 100),
                        ProductCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductSubCategoryId)
                .ForeignKey("dbo.ProductCategories", t => t.ProductCategoryId)
                .Index(t => t.ProductCategoryId);
            
            CreateTable(
                "dbo.Programs",
                c => new
                    {
                        ProgramId = c.Int(nullable: false, identity: true),
                        ProgramName = c.String(nullable: false, maxLength: 100),
                        ProductSubCategory_ProductSubCategoryId = c.Int(),
                        ProductCategory_ProductCategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.ProgramId)
                .ForeignKey("dbo.ProductSubCategories", t => t.ProductSubCategory_ProductSubCategoryId)
                .ForeignKey("dbo.ProductCategories", t => t.ProductCategory_ProductCategoryId)
                .Index(t => t.ProductSubCategory_ProductSubCategoryId)
                .Index(t => t.ProductCategory_ProductCategoryId);
            
            CreateTable(
                "dbo.Inventories",
                c => new
                    {
                        InventoryId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        QuantityInStock = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InventoryId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        OrderItemId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OrderItemId)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        OrderNo = c.String(nullable: false, maxLength: 50),
                        OrderDate = c.DateTime(nullable: false),
                        IsPaid = c.Boolean(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(nullable: false, maxLength: 100),
                        Email = c.String(),
                        MobileNo = c.String(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewId = c.Int(nullable: false, identity: true),
                        ReviewerName = c.String(nullable: false, maxLength: 100),
                        Content = c.String(nullable: false, maxLength: 1000),
                        Rating = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        CartId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.CartId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartItems", "Cart_CartId", "dbo.Carts");
            DropForeignKey("dbo.CartItems", "Product_ProductId", "dbo.Products");
            DropForeignKey("dbo.Reviews", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "ProgramId", "dbo.Programs");
            DropForeignKey("dbo.Products", "ProductSubCategoryId", "dbo.ProductSubCategories");
            DropForeignKey("dbo.Products", "ProductCategoryId", "dbo.ProductCategories");
            DropForeignKey("dbo.OrderItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Inventories", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "DiscountId", "dbo.Discounts");
            DropForeignKey("dbo.Discounts", "ProductCategoryId", "dbo.ProductCategories");
            DropForeignKey("dbo.Programs", "ProductCategory_ProductCategoryId", "dbo.ProductCategories");
            DropForeignKey("dbo.Programs", "ProductSubCategory_ProductSubCategoryId", "dbo.ProductSubCategories");
            DropForeignKey("dbo.ProductSubCategories", "ProductCategoryId", "dbo.ProductCategories");
            DropIndex("dbo.Reviews", new[] { "ProductId" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropIndex("dbo.OrderItems", new[] { "ProductId" });
            DropIndex("dbo.OrderItems", new[] { "OrderId" });
            DropIndex("dbo.Inventories", new[] { "ProductId" });
            DropIndex("dbo.Programs", new[] { "ProductCategory_ProductCategoryId" });
            DropIndex("dbo.Programs", new[] { "ProductSubCategory_ProductSubCategoryId" });
            DropIndex("dbo.ProductSubCategories", new[] { "ProductCategoryId" });
            DropIndex("dbo.Discounts", new[] { "ProductCategoryId" });
            DropIndex("dbo.Products", new[] { "DiscountId" });
            DropIndex("dbo.Products", new[] { "ProductSubCategoryId" });
            DropIndex("dbo.Products", new[] { "ProductCategoryId" });
            DropIndex("dbo.Products", new[] { "ProgramId" });
            DropIndex("dbo.CartItems", new[] { "Cart_CartId" });
            DropIndex("dbo.CartItems", new[] { "Product_ProductId" });
            DropTable("dbo.Carts");
            DropTable("dbo.Reviews");
            DropTable("dbo.Customers");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderItems");
            DropTable("dbo.Inventories");
            DropTable("dbo.Programs");
            DropTable("dbo.ProductSubCategories");
            DropTable("dbo.ProductCategories");
            DropTable("dbo.Discounts");
            DropTable("dbo.Products");
            DropTable("dbo.CartItems");
        }
    }
}
