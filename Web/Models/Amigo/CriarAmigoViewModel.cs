using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Amigo
{
    public class CriarAmigoViewModel
    {
        public IFormFile Foto { get; set; }
        public string UrlFoto { get; set; }
        public string Name { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime DtAniversario { get; set; }
        public Pais.ListarPaisViewModel Pais { get; set; }
        public Estado.ListarEstadoViewModel Estado { get; set; }

        public List<string> Errors { get; set; }
    }
}
