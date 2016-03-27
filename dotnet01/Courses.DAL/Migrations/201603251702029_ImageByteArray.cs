namespace Courses.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageByteArray : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Image", c => c.Binary());
            DropColumn("dbo.Products", "imagePath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "imagePath", c => c.String());
            DropColumn("dbo.Products", "Image");
        }
    }
}
