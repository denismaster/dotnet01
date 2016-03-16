namespace Courses.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ParentCategoryId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "ParentCategoryId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "ParentCategoryId");
        }
    }
}
