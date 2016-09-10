using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoMapper.Demo.Models.Dominio
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string Bio { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }

        public string PegaNome()
        {
            return Nome + ' ' + Sobrenome;
        } 
    }
}