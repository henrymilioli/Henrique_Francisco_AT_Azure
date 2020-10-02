using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Paises.Models.Estado
{
    public class EstadoResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UrlFoto { get; set; }
        public string Pais { get; set; }
    }
}
