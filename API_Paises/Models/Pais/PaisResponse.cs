using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Paises.Models.Pais
{
    public class PaisResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string UrlFoto { get; set; }
    }
}
