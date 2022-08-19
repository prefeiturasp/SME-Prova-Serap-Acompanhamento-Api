using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using SME.SERAp.Prova.Acompanhamento.Infra.Exceptions;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class AutenticacaoRevalidarUseCase : AbstractUseCase, IAutenticacaoRevalidarUseCase
    {
        public AutenticacaoRevalidarUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<AutenticacaoRetornoDto> Executar(AutenticacaoRevalidarDto autenticacaoRevalidarDto)
        {
            var abrangencias = await mediator.Send(new ObterInformacoesPorTokenJwtQuery(autenticacaoRevalidarDto.Token));

            if (abrangencias == null || !abrangencias.Any())
                throw new NaoAutorizadoException("Token inválido", 401);

            return await mediator.Send(new ObterTokenJwtQuery(abrangencias));
        }
    }
}
