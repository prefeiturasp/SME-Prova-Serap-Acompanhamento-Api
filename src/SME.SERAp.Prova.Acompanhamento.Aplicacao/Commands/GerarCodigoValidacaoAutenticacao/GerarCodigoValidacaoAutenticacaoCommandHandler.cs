using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Cache;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class GerarCodigoValidacaoAutenticacaoCommandHandler : IRequestHandler<GerarCodigoValidacaoAutenticacaoCommand, AutenticacaoValidarDto>
    {
        public readonly IRepositorioCache repositorioCache;

        public GerarCodigoValidacaoAutenticacaoCommandHandler(IRepositorioCache repositorioCache)
        {
            this.repositorioCache = repositorioCache ?? throw new ArgumentNullException(nameof(repositorioCache));
        }

        public async Task<AutenticacaoValidarDto> Handle(GerarCodigoValidacaoAutenticacaoCommand request, CancellationToken cancellationToken)
        {
            var codigo = Guid.NewGuid();
            var chave = string.Format(CacheChave.Autenticacao, codigo);

            await repositorioCache.SalvarRedisAsync(chave, request.Abrangencias);

            return new AutenticacaoValidarDto(codigo.ToString());
        }
    }
}
