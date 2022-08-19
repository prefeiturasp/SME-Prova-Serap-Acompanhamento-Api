using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Constraints;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using SME.SERAp.Prova.Acompanhamento.Infra.Exceptions;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class AutenticacaoUseCase : AbstractUseCase, IAutenticacaoUseCase
    {
        public AutenticacaoUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<AutenticacaoValidarDto> Executar(AutenticacaoDto autenticacaoDto)
        {
            if (!Perfis.PerfilEhValido(autenticacaoDto.Perfil))
                throw new NaoAutorizadoException("Grupo inválido", 401);

            var abrangencias = await mediator.Send(new ObterAbrangenciaPorLoginGrupoQuery(autenticacaoDto.Login, autenticacaoDto.Perfil));

            if (abrangencias == null || !abrangencias.Any())
                throw new NaoAutorizadoException("Usuário inválido", 401);

            return await mediator.Send(new GerarCodigoValidacaoAutenticacaoCommand(abrangencias));
        }
    }
}
