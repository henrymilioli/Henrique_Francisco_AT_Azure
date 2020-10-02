using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Amigos.Models
{
    public class AmizadeRequest
    {
        public string AmigoSolicitacaoId { get; set; }
        public Domain.Amigo.Amigo Amigo { get; set; }
        public List<string> Errors()
        {
            var listErro = new List<string>();

            if (string.IsNullOrEmpty(Amigo.Id.ToString()))
            {
                listErro.Add("Preencha o nome!");
            }

            return listErro;
        }
    }
}
