using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Infra.Dtos
{
    public class TotalProvasDto
    {
        public List<TotalDto> Totais { get; set; }
        public GraficosDto Graficos { get; set; }
    }
}
