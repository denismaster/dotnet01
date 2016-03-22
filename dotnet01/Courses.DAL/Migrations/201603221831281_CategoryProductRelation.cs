namespace Courses.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryProductRelation : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.CategoryProducts", name: "Category_CategoryId", newName: "CategoryId");
            RenameColumn(table: "dbo.CategoryProducts", name: "Product_Id", newName: "ProductId");
            RenameIndex(table: "dbo.CategoryProducts", name: "IX_Category_CategoryId", newName: "IX_CategoryId");
            RenameIndex(table: "dbo.CategoryProducts", name: "IX_Product_Id", newName: "IX_ProductId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.CategoryProducts", name: "IX_ProductId", newName: "IX_Product_Id");
            RenameIndex(table: "dbo.CategoryProducts", name: "IX_CategoryId", newName: "IX_Category_CategoryId");
            RenameColumn(table: "dbo.CategoryProducts", name: "ProductId", newName: "Product_Id");
            RenameColumn(table: "dbo.CategoryProducts", name: "CategoryId", newName: "Category_CategoryId");
        }
    }
}
