using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class ObterTodosTesteUseCase : AbstractUseCase, IObterTodosTesteUseCase
    {
        public ObterTodosTesteUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<IEnumerable<Teste>> Executar()
        {
            return await mediator.Send(new ObterTodosTesteQuery());
        }
    }
}
