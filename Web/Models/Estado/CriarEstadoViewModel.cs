using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Web.Models.Estado
{
    public class CriarEstadoViewModel
    {
        public IFormFile Foto { get; set; }
        public string UrlFoto { get; set; }
        public string Name { get; set; }
        public Pais.ListarPaisViewModel Pais { get; set; }
        public List<string> Errors { get; set; }
    }
}
