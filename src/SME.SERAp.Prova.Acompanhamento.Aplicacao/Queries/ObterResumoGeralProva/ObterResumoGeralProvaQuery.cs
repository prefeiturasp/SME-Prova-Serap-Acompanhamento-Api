using MediatR;
using SME.SERAp.Prova.Acompanhamento.Infra;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterResumoGeralProvaQuery : IRequest<IEnumerable<ResumoGeralProvaDto>>
    {
        public ObterResumoGeralProvaQuery(FiltroDto filtro)
        {
            Filtro = filtro;
        }
        public FiltroDto Filtro { get; set; }
    }
}
