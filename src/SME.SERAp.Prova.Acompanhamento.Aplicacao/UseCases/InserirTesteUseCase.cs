using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class InserirTesteUseCase : AbstractUseCase, IInserirTesteUseCase
    {
        public InserirTesteUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(InserirTesteDto inserirTesteDto)
        {
            return await mediator.Send(new InserirTesteCommand(inserirTesteDto.Descricao));
        }
    }
}
