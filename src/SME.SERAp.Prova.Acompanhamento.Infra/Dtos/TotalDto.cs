using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Infra.Dtos
{
    public class TotalDto
    {
        public TotalDto(string  titulo, string cor, string valor)
        {
            Titulo = titulo;
            Cor = cor;
            Valor = valor;
        }
        public string Titulo { get; set; }
        public string Cor { get; set; }
        public string Valor { get; set; }
    }
}
