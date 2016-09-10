using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using AutoMapper.Demo.Models.Dominio;

namespace AutoMapper.Demo.Models.Contexto
{
    public class CriarDados : DropCreateDatabaseAlways<ContextoDB>
    {
        protected override void Seed(ContextoDB context)
        {
            PopularDados(context);
           

            base.Seed(context);
        }

       
        private static void PopularDados(ContextoDB context)
        {
            try
            {
               // context.Database.Log = Console.Write;

               var listaClientes = new List<Cliente>
                {
                    new Cliente {Nome = "Donisetti", Sobrenome = "Ferreira Cosma"},
                    new Cliente {Nome = "Luiz Fernando", Sobrenome = "Pientka"},
                    new Cliente {Nome = "Fabiana", Sobrenome = "de Cosma Fernandes"},
                    new Cliente {Nome = "Solange", Sobrenome = "Ferreira Cosma"},
                    new Cliente {Nome = "Soleide", Sobrenome = "Ferreira Cosma"},
                    new Cliente {Nome = "Valdir", Sobrenome = "Antonio da Silva"},
                    new Cliente {Nome = "André", Sobrenome = "Bineli",Bio = "Analista de sistemas"}
                };

                listaClientes.ForEach(i => context.Clientes.AddOrUpdate(i));

                context.SaveChanges();

                var listaPedidos = new List<Pedido>
                {
                    new Pedido
                    {
                        Cliente = context.Clientes.FirstOrDefault(x=>x.Nome.Contains("André")),
                        NumeroPedido = "000.002",
                        DataCompra = new DateTime(2015, 6, 28),
                        Entregar = false,
                        InternalId = new Guid(),
                        LinhaPedido = new List<ItensPedido>
                        {
                                new ItensPedido
                                {                                    
                                    Preco = 2.4m,
                                    Produto = "Pão de Frances",
                                    Quantidade = 13
                                },
                                  new ItensPedido
                                {                                    
                                    Preco = 5.4m,
                                    Produto = "Café",
                                    Quantidade = 13
                                }
                        }
                       
                    },

                    new Pedido
                    {
                        Cliente = context.Clientes.Find(1),
                        NumeroPedido = "000.001",
                        DataCompra = new DateTime(2015, 6, 28),
                        Entregar = false,
                        InternalId = new Guid(),
                       
                    }
                };

                listaPedidos.ForEach(i => context.Pedidos.AddOrUpdate(i));
                context.SaveChanges();

                var pedido = context.Pedidos.FirstOrDefault(x => x.NumeroPedido == "000.001");

                var linhasPedido = new List<ItensPedido>
                {
                    new ItensPedido
                    {
                        Pedido = pedido,
                        Preco = 5.4m,
                        Produto = "Pão de Queijo",
                        Quantidade = 3
                    },

                    new ItensPedido
                    {
                        Pedido = pedido,
                        Preco = 10.4m,
                        Produto = "Açucar Cristal 5 KG",
                        Quantidade = 5
                    },

                      new ItensPedido
                    {
                        Pedido = pedido,
                        Preco = 8.54m,
                        Produto = "Sabão em pó 1 KG",
                        Quantidade = 7
                    },

                      new ItensPedido
                    {
                        Pedido = pedido,
                        Preco =3.74m,
                        Produto = "Oleo de Soja",
                        Quantidade = 9
                    }
                };

                linhasPedido.ForEach(p => context.ItensPedidos.AddOrUpdate(p));

                context.SaveChanges();
            }
            catch (Exception erro)
            {

                throw;
            }

        }
    }
}