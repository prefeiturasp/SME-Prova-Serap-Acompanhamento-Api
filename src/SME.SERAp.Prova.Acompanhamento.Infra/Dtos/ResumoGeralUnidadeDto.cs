using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Infra.Dtos
{
    public class ResumoGeralUnidadeDto
    {

        public long Id { get; set; }
        public string Nome { get; set; }

        public ResumoGeralProvaDto Item { get; set; }
   
    }

}

