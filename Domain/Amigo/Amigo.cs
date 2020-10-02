using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Amigo
{
    public class Amigo
    {
        public Guid Id { get; set; }
        public string UrlFoto { get; set; }
        public string Name { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime DtAniversario { get; set; }
        public Pais.Pais Pais { get; set; }
        public Estado.Estado Estado { get; set; }
        [JsonIgnore]
        public List<Amizade> Amizade { get; set; } 
    }
}
