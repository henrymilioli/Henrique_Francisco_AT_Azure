using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Pais
{
    public class Pais
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string UrlFoto { get; set; }

        [JsonIgnore]
        public List<Estado.Estado> ListEstado { get; set; }
    }
}
