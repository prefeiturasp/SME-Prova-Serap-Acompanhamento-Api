using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class GerarCodigoValidacaoAutenticacaoCommandHandler : IRequestHandler<GerarCodigoValidacaoAutenticacaoCommand, AutenticacaoValidarDto>
    {
        public readonly IRepositorioAutenticacao repositorioAutenticacao;

        public GerarCodigoValidacaoAutenticacaoCommandHandler(IRepositorioAutenticacao repositorioAutenticacao)
        {
            this.repositorioAutenticacao = repositorioAutenticacao;
        }

        public async Task<AutenticacaoValidarDto> Handle(GerarCodigoValidacaoAutenticacaoCommand request, CancellationToken cancellationToken)
        {
            var codigo = Guid.NewGuid();
            await repositorioAutenticacao.InserirAsync(new Autenticacao(codigo, request.Abrangencias));
            return new AutenticacaoValidarDto(codigo.ToString());
        }
    }
}
