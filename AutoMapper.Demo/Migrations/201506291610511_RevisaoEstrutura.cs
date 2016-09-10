namespace AutoMapper.Demo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RevisaoEstrutura : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItensPedido", "PedidoId", c => c.Int(nullable: false));
            CreateIndex("dbo.ItensPedido", "PedidoId");
            AddForeignKey("dbo.ItensPedido", "PedidoId", "dbo.Pedido", "PedidoId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItensPedido", "PedidoId", "dbo.Pedido");
            DropIndex("dbo.ItensPedido", new[] { "PedidoId" });
            DropColumn("dbo.ItensPedido", "PedidoId");
        }
    }
}
