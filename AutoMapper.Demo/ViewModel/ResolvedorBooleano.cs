using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper.Demo.Models.Dominio;

namespace AutoMapper.Demo.ViewModel
{
    public class ResolvedorBooleano : IValueResolver<Pedido, PedidoDTO, string>
    {
        public string Resolve(Pedido source, PedidoDTO destination, string destMember, ResolutionContext context)
        {
            return source.Entregar ? "Sim" : "Não";
        }
    }
}

