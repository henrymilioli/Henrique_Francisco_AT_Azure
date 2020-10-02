using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Amigos.Models
{
    public class AmigoResquest
    {
        public string UrlFoto { get; set; }
        public string Name { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime DtAniversario { get; set; }
        public Domain.Pais.Pais Pais{ get; set; }
        public Domain.Estado.Estado Estado { get; set; }
        public List<string> Errors()
        {
            var listErro = new List<string>();

            if (string.IsNullOrEmpty(Name))
            {
                listErro.Add("Preencha o nome!");
            }

            return listErro;
        }
    }
}