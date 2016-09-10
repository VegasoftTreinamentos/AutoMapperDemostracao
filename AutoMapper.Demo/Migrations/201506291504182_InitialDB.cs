namespace AutoMapper.Demo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        ClienteId = c.Int(nullable: false, identity: true),
                        Bio = c.String(maxLength: 150, unicode: false),
                        Nome = c.String(maxLength: 150, unicode: false),
                        Sobrenome = c.String(maxLength: 150, unicode: false),
                    })
                .PrimaryKey(t => t.ClienteId);
            
            CreateTable(
                "dbo.ItensPedido",
                c => new
                    {
                        ItemId = c.Int(nullable: false, identity: true),
                        Preco = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Produto = c.String(maxLength: 150, unicode: false),
                        Quantidade = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemId);
            
            CreateTable(
                "dbo.Pedido",
                c => new
                    {
                        PedidoId = c.Int(nullable: false, identity: true),
                        NumeroPedido = c.String(maxLength: 150, unicode: false),
                        DataCompra = c.DateTime(nullable: false),
                        Entregar = c.Boolean(nullable: false),
                        InternalId = c.Guid(nullable: false),
                        Cliente_ClienteId = c.Int(),
                    })
                .PrimaryKey(t => t.PedidoId)
                .ForeignKey("dbo.Cliente", t => t.Cliente_ClienteId)
                .Index(t => t.Cliente_ClienteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pedido", "Cliente_ClienteId", "dbo.Cliente");
            DropIndex("dbo.Pedido", new[] { "Cliente_ClienteId" });
            DropTable("dbo.Pedido");
            DropTable("dbo.ItensPedido");
            DropTable("dbo.Cliente");
        }
    }
}
