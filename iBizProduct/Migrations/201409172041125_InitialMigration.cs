namespace iBizProduct.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        ExternalKey = c.String(),
                        Name = c.String(),
                        UriName = c.String(),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductSettings",
                c => new
                    {
                        ProductSettingId = c.Guid(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Key = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.ProductSettingId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductSettings", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductSettings", new[] { "ProductId" });
            DropTable("dbo.ProductSettings");
            DropTable("dbo.Products");
        }
    }
}
