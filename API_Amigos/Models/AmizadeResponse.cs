using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Amigos.Models
{
    public class AmizadeResponse
    {
        public Guid Id { get; set; }
        public string AmigoSolicitacaoId { get; set; }
        public Domain.Amigo.Amigo Amigo { get; set; }

    }
}
