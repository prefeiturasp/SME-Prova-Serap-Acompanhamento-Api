using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Infra.Dtos
{
    public class ResumoGeralDreDto
    {

        public long DreId { get; set; }
        public string DreNome { get; set; }

        public ResumoGeralProvaDto Item { get; set; }
   
    }

}

