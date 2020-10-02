using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Amigos.Models
{
    public class AmigoResponse
    {
        public Guid Id { get; set; }
        public string UrlFoto { get; set; }
        public string Name { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime DtAniversario { get; set; }
        public string Pais { get; set; }
        public string Estado { get; set; }
    }
}