using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Estado
{
    public class Estado
    {
        public Guid Id { get; set; }
        public string UrlFoto { get; set; }
        public string Name { get; set; }
        public Pais.Pais Pais { get; set; }
    }
}
