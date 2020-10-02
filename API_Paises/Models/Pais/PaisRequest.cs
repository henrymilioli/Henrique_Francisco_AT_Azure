using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Paises.Models.Pais
{
    public class PaisRequest
    {
        public string Nome { get; set; }
        public string UrlFoto { get; set; }
        
        public List<string> Errors()
        {
            var listErro = new List<string>();

            if (string.IsNullOrEmpty(Nome))
            {
                listErro.Add("Nome precisa ser preenchido.");
            }

            return listErro;
        }
    }
}
