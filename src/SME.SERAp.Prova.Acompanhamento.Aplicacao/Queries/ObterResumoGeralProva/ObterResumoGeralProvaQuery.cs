using MediatR;
using SME.SERAp.Prova.Acompanhamento.Infra;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterResumoGeralProvaQuery : IRequest<IEnumerable<ResumoGeralProvaDto>>
    {
        public ObterResumoGeralProvaQuery()
        {

        }
    }
}
