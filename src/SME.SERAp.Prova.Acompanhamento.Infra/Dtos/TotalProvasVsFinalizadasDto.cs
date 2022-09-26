using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Infra.Dtos
{
    public class TotalProvasVsFinalizadasDto
    {

        public string ProvaDescricao { get; set; }
        public long TotalProvas { get; set; }
        public long TotalProvasFinalizadas { get; set; }
    }
}
