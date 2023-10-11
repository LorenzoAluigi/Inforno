namespace Inforno.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArticoliOrdine",
                c => new
                    {
                        IDArticolo = c.Int(nullable: false, identity: true),
                        IDOrdine = c.Int(nullable: false),
                        IDProdotto = c.Int(nullable: false),
                        Quantita = c.Int(nullable: false),
                        PrezzoUnitario = c.Decimal(nullable: false, precision: 10, scale: 2),
                    })
                .PrimaryKey(t => t.IDArticolo)
                .ForeignKey("dbo.Ordini", t => t.IDOrdine)
                .ForeignKey("dbo.Prodotti", t => t.IDProdotto)
                .Index(t => t.IDOrdine)
                .Index(t => t.IDProdotto);
            
            CreateTable(
                "dbo.Ordini",
                c => new
                    {
                        IDOrdine = c.Int(nullable: false, identity: true),
                        IDUser = c.Int(nullable: false),
                        DataOrdine = c.DateTime(nullable: false),
                        IndirizzoSpedizione = c.String(nullable: false, maxLength: 255),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.IDOrdine)
                .ForeignKey("dbo.Users", t => t.IDUser)
                .Index(t => t.IDUser);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        IDUser = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 255),
                        Cognome = c.String(nullable: false, maxLength: 255),
                        Provincia = c.String(nullable: false, maxLength: 255),
                        Citta = c.String(nullable: false, maxLength: 255),
                        Indirizzo = c.String(nullable: false, maxLength: 255),
                        Email = c.String(nullable: false, maxLength: 255),
                        Password = c.String(nullable: false, maxLength: 255),
                        IDRuolo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IDUser)
                .ForeignKey("dbo.Ruoli", t => t.IDRuolo)
                .Index(t => t.IDRuolo);
            
            CreateTable(
                "dbo.Ruoli",
                c => new
                    {
                        idRuolo = c.Int(nullable: false, identity: true),
                        Ruolo = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.idRuolo);
            
            CreateTable(
                "dbo.Prodotti",
                c => new
                    {
                        IDProdotto = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 255),
                        Foto = c.String(),
                        PrezzoVendita = c.Decimal(nullable: false, precision: 10, scale: 2),
                        TempoConsegna = c.Time(precision: 7),
                        Ingredienti = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IDProdotto);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ArticoliOrdine", "IDProdotto", "dbo.Prodotti");
            DropForeignKey("dbo.Users", "IDRuolo", "dbo.Ruoli");
            DropForeignKey("dbo.Ordini", "IDUser", "dbo.Users");
            DropForeignKey("dbo.ArticoliOrdine", "IDOrdine", "dbo.Ordini");
            DropIndex("dbo.Users", new[] { "IDRuolo" });
            DropIndex("dbo.Ordini", new[] { "IDUser" });
            DropIndex("dbo.ArticoliOrdine", new[] { "IDProdotto" });
            DropIndex("dbo.ArticoliOrdine", new[] { "IDOrdine" });
            DropTable("dbo.Prodotti");
            DropTable("dbo.Ruoli");
            DropTable("dbo.Users");
            DropTable("dbo.Ordini");
            DropTable("dbo.ArticoliOrdine");
        }
    }
}
