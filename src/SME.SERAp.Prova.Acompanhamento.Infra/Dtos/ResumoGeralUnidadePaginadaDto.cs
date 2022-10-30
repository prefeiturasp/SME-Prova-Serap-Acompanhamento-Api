
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Infra.Dtos
{
    public class ResumoGeralUnidadePaginadaDto
    {
        public List<ResumoGeralUnidadeDto> Items { get; set; }
        public int TotalPaginas { get; set; }
        public int TotalRegistros { get; set; }

    }
}
