using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Amigo
{
    public class CriarAmizadeViewModel
    {
        public ListarAmigoViewModel Amigo { get; set; }

        public List<string> Errors { get; set; }
    }
}
