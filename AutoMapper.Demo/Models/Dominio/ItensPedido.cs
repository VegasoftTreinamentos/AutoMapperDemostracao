using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoMapper.Demo.Models.Dominio
{
    public class ItensPedido
    {
      
        public decimal Preco { get; set; }
        public string Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoTotal()
        {
            return Preco * Quantidade;
        }

        [Key]
        public int ItemId { get; set; }

        //Chave Estrangeira
        public int PedidoId { get; set; }

        public virtual Pedido Pedido { get; set; }

     


    }
}