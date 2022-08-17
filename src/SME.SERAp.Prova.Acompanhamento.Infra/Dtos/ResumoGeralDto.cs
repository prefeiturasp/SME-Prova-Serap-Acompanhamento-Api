using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Infra
{
    public class ResumoGeralDto
    {
        public List<ResumoGeralProvaDto> Items { get; set; }
        public int TotalPaginas { get; set; }
        public int TotalRegistros { get; set; }
    }
}
