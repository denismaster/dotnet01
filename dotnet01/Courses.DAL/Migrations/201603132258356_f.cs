namespace Courses.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class f : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "Description", c => c.String());
            DropColumn("dbo.Categories", "ParentId");
            AddColumn("dbo.Products", "imagePath", c => c.String());
        }
        
        public override void Down()
        {
           
            DropColumn("dbo.Categories", "Description");
            
            
        }
    }
}
