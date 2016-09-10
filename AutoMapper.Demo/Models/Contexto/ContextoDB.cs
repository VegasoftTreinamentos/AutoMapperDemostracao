using System.Data.Entity.ModelConfiguration.Conventions;
using AutoMapper.Demo.Models.Dominio;

using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace AutoMapper.Demo.Models.Contexto
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ContextoDB : DbContext
    {
        public ContextoDB()
            : base("name=ContextoDB")
        {
            Database.SetInitializer(new CriarDados());
           

        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItensPedido> ItensPedidos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();


            modelBuilder.Properties()
                .Where(p => p.ReflectedType != null && p.Name == p.ReflectedType.Name + "Id")
                .Configure(p => p.IsKey());

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(150));

           
        }
    }
}
