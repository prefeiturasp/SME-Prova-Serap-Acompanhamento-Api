using MediatR;
using SME.SERAp.Prova.Acompanhamento.Infra;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterResumoGeralProvaQuery : IRequest<IEnumerable<ResumoGeralProvaDto>>
    {
        public ObterResumoGeralProvaQuery(FiltroDto filtro, long[] dresId, long[] uesId)
        {
            Filtro = filtro;
            DresId = dresId;
            UesId = uesId;
        }

        public FiltroDto Filtro { get; set; }

        public long[] DresId { get; set; }
        public long[] UesId { get; set; }
    }
}
