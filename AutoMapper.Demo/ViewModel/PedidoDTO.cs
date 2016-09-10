using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AutoMapper.Demo.Models.Dominio;

namespace AutoMapper.Demo.ViewModel
{
    public class PedidoDTO
    {
        [Display(Name = "Nome do Cliente")]
        public string NomeCliente { get; set; }
        [Display(Name = "Total do Pedido")]
        public decimal Total { get; set; }
        [Display(Name = "Número")]
        public string NrPedido { get; set; }

        [Display(Name = "Entregar?")]
        public string Entregar { get; set; }

        [Display(Name = "Itens do Pedido")]
        public virtual IEnumerable<ItensPedido> LinhaPedido { get; set; }
    }
}