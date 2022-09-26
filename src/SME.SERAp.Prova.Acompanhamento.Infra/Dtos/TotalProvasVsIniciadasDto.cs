using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Infra.Dtos
{
    public class TotalProvasVsIniciadasDto
    {
        public string ProvaDescricao { get; set; }
        public long TotalProvas { get; set; }
        public long TotalProvasIniciadas { get; set; }
    }
}
