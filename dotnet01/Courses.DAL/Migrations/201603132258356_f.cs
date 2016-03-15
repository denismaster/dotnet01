namespace Courses.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class f : DbMigration
    {
        public override void Up()
        {
      

            AddColumn("dbo.Categories", "Description", c => c.String());
       
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "ParentId", c => c.Int());
            DropColumn("dbo.Categories", "Description");
            RenameIndex(table: "dbo.Categories", name: "IX_ParentCategory_CategoryId", newName: "IX__Category_CategoryId");
            RenameColumn(table: "dbo.Categories", name: "ParentCategory_CategoryId", newName: "_Category_CategoryId");
        }
    }
}
