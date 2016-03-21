namespace Courses.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AppointmentId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        OrderId = c.Int(nullable: false),
                        SumTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Appointments", t => t.AppointmentId, cascadeDelete: true)
                .Index(t => t.AppointmentId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        AuthKey = c.String(),
                        PasswordHash = c.String(),
                        PasswordResetToken = c.String(),
                        Email = c.String(),
                        Role = c.Byte(nullable: false),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        Gender = c.Boolean(nullable: false),
                        City = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Type = c.Int(nullable: false),
                        PartnerId = c.Int(nullable: false),
                        Teacher = c.String(),
                        SeatsCount = c.Int(),
                        AssignedUserId = c.Int(),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Partners", t => t.PartnerId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.AssignedUserId)
                .Index(t => t.PartnerId)
                .Index(t => t.AssignedUserId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                        ParentId = c.Int(),
                        _Category_CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.CategoryId)
                .ForeignKey("dbo.Categories", t => t._Category_CategoryId)
                .Index(t => t._Category_CategoryId);
            
            CreateTable(
                "dbo.Partners",
                c => new
                    {
                        PartnerId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        UserId = c.Int(),
                        Address = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Contact = c.String(),
                    })
                .PrimaryKey(t => t.PartnerId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        AuthKey = c.String(),
                        PasswordHash = c.String(),
                        Email = c.String(),
                        Role = c.String(),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Entity = c.String(),
                        Changes = c.String(),
                        UserId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ProductRatings",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        Id = c.Int(nullable: false),
                        Rate = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.CustomerId })
                .ForeignKey("dbo.Products", t => t.Id, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.EmailQueues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NewsletterId = c.Int(),
                        CustomerId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmailNewsletters", t => t.NewsletterId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.NewsletterId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.EmailNewsletters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TemplateId = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        UdpatedDate = c.DateTime(nullable: false),
                        Name = c.String(),
                        Enabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmailTemplates", t => t.TemplateId)
                .Index(t => t.TemplateId);
            
            CreateTable(
                "dbo.EmailTemplates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Text = c.String(),
                        ParentId = c.Int(),
                        AppointmentId = c.Int(nullable: false),
                        _Schedule_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Schedules", t => t._Schedule_Id)
                .ForeignKey("dbo.Appointments", t => t.AppointmentId, cascadeDelete: true)
                .Index(t => t.AppointmentId)
                .Index(t => t._Schedule_Id);
            
            CreateTable(
                "dbo.PartnerCategories",
                c => new
                    {
                        Partner_PartnerId = c.Int(nullable: false),
                        Category_CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Partner_PartnerId, t.Category_CategoryId })
                .ForeignKey("dbo.Partners", t => t.Partner_PartnerId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_CategoryId, cascadeDelete: true)
                .Index(t => t.Partner_PartnerId)
                .Index(t => t.Category_CategoryId);
            
            CreateTable(
                "dbo.CategoryProducts",
                c => new
                    {
                        Category_CategoryId = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_CategoryId, t.Product_Id })
                .ForeignKey("dbo.Categories", t => t.Category_CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.Category_CategoryId)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.FavouriteProducts",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.CustomerId })
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedules", "AppointmentId", "dbo.Appointments");
            DropForeignKey("dbo.Schedules", "_Schedule_Id", "dbo.Schedules");
            DropForeignKey("dbo.OrderItems", "AppointmentId", "dbo.Appointments");
            DropForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.ProductRatings", "Id", "dbo.Customers");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.EmailQueues", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.EmailNewsletters", "TemplateId", "dbo.EmailTemplates");
            DropForeignKey("dbo.EmailQueues", "NewsletterId", "dbo.EmailNewsletters");
            DropForeignKey("dbo.Comments", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.ProductRatings", "Id", "dbo.Products");
            DropForeignKey("dbo.FavouriteProducts", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.FavouriteProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Comments", "ProductId", "dbo.Products");
            DropForeignKey("dbo.CategoryProducts", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.CategoryProducts", "Category_CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Products", "AssignedUserId", "dbo.Users");
            DropForeignKey("dbo.Partners", "UserId", "dbo.Users");
            DropForeignKey("dbo.Events", "UserId", "dbo.Users");
            DropForeignKey("dbo.Products", "PartnerId", "dbo.Partners");
            DropForeignKey("dbo.PartnerCategories", "Category_CategoryId", "dbo.Categories");
            DropForeignKey("dbo.PartnerCategories", "Partner_PartnerId", "dbo.Partners");
            DropForeignKey("dbo.Categories", "_Category_CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Appointments", "ProductId", "dbo.Products");
            DropIndex("dbo.FavouriteProducts", new[] { "CustomerId" });
            DropIndex("dbo.FavouriteProducts", new[] { "ProductId" });
            DropIndex("dbo.CategoryProducts", new[] { "Product_Id" });
            DropIndex("dbo.CategoryProducts", new[] { "Category_CategoryId" });
            DropIndex("dbo.PartnerCategories", new[] { "Category_CategoryId" });
            DropIndex("dbo.PartnerCategories", new[] { "Partner_PartnerId" });
            DropIndex("dbo.Schedules", new[] { "_Schedule_Id" });
            DropIndex("dbo.Schedules", new[] { "AppointmentId" });
            DropIndex("dbo.EmailNewsletters", new[] { "TemplateId" });
            DropIndex("dbo.EmailQueues", new[] { "CustomerId" });
            DropIndex("dbo.EmailQueues", new[] { "NewsletterId" });
            DropIndex("dbo.ProductRatings", new[] { "Id" });
            DropIndex("dbo.Events", new[] { "UserId" });
            DropIndex("dbo.Partners", new[] { "UserId" });
            DropIndex("dbo.Categories", new[] { "_Category_CategoryId" });
            DropIndex("dbo.Products", new[] { "AssignedUserId" });
            DropIndex("dbo.Products", new[] { "PartnerId" });
            DropIndex("dbo.Comments", new[] { "ProductId" });
            DropIndex("dbo.Comments", new[] { "CustomerId" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropIndex("dbo.OrderItems", new[] { "OrderId" });
            DropIndex("dbo.OrderItems", new[] { "AppointmentId" });
            DropIndex("dbo.Appointments", new[] { "ProductId" });
            DropTable("dbo.FavouriteProducts");
            DropTable("dbo.CategoryProducts");
            DropTable("dbo.PartnerCategories");
            DropTable("dbo.Schedules");
            DropTable("dbo.EmailTemplates");
            DropTable("dbo.EmailNewsletters");
            DropTable("dbo.EmailQueues");
            DropTable("dbo.ProductRatings");
            DropTable("dbo.Events");
            DropTable("dbo.Users");
            DropTable("dbo.Partners");
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.Comments");
            DropTable("dbo.Customers");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderItems");
            DropTable("dbo.Appointments");
        }
    }
}
