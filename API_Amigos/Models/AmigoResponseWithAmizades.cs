﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Amigos.Models
{
    public class AmigoResponseWithAmizades
    {
        public Guid Id { get; set; }
        public string UrlFoto { get; set; }
        public string Name { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime DtAniversario { get; set; }
        public string Pais { get; set; }
        public string Estado { get; set; }
        public List<string> Amigo { get; set; }
    }
}
