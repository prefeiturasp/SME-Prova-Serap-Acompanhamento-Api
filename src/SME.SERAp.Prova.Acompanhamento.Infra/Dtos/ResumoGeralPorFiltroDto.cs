using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Infra.Dtos
{
    public class ResumoGeralPorFiltroDto
    {
        public string ScrollId { get; set; }
        public IEnumerable<ProvaTurmaResultado> Resultados { get; set; }
    }
}
