using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Estado
{
    public class DetailsEstadoViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UrlFoto { get; set; }
        public string Pais { get; set; }
    }
}
