﻿namespace Inforno.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeTimeType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Prodotti", "TempoConsegna", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Prodotti", "TempoConsegna", c => c.Time(precision: 7));
        }
    }
}
