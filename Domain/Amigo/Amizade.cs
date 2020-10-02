using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Amigo
{
    public class Amizade
    {
        public Guid Id { get; set; }
        public string AmigoSolicitacaoId { get; set; }
        public Amigo Amigo { get; set; }
    }
}
