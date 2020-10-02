using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Paises.Models.Estado
{
    public class EstadoRequest
    {
        public string Name { get; set; }
        public string UrlFoto { get; set; }
        public Domain.Pais.Pais Pais { get; set; }
        public List<string> Errors()
        {
            var listErro = new List<string>();

            if (string.IsNullOrEmpty(Name))
            {
                listErro.Add("Nome precisa ser preenchido.");
            }

            return listErro;
        }
    }
}
