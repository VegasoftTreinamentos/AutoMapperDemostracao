using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoMapper.Demo.ViewModel
{
    public class ClienteDTO
    {
        [Display(Name = "Código")]
        public int ClienteId { get; set; }
        public string Bio { get; set; }
        public string Nome { get; set; }
          [Display(Name = "Nome Completo")]
        public string NomeCompleto { get; set; }
    }
}