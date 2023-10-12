namespace Inforno.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddISEvasoColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ordini", "IsEvaso", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ordini", "IsEvaso");
        }
    }
}
