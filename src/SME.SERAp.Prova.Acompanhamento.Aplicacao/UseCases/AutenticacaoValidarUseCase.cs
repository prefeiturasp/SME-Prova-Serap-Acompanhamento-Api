using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using SME.SERAp.Prova.Acompanhamento.Infra.Exceptions;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class AutenticacaoValidarUseCase : AbstractUseCase, IAutenticacaoValidarUseCase
    {
        public AutenticacaoValidarUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<AutenticacaoRetornoDto> Executar(AutenticacaoValidarDto autenticacaoValidarDto)
        {
            var abrangencias = await mediator.Send(new ObterAbrangenciaPorCodigoValidacaoAutenticacaoQuery(autenticacaoValidarDto.Codigo));

            if (abrangencias == null || !abrangencias.Any())
                throw new NaoAutorizadoException("Código inválido", 401);

            await mediator.Send(new RemoverCodigoValidacaoAutenticacaoCommand(autenticacaoValidarDto.Codigo));

            return await mediator.Send(new ObterTokenJwtQuery(abrangencias));
        }
    }
}
