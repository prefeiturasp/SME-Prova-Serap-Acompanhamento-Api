using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases;
using SME.SERAp.Prova.Acompanhamento.Infra;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ResumoGeralProvasUse : AbstractUseCase, IResumoGeralProvasUse
    {
        public ResumoGeralProvasUse(IMediator mediator) : base(mediator){}

        public async Task<IEnumerable<ResumoGeralProvaDto>> Executar()
        {
            return default;
        }
    }
}
