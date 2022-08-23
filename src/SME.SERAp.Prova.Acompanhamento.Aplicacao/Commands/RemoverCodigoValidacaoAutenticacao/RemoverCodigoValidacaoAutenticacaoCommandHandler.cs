using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Cache;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class RemoverCodigoValidacaoAutenticacaoCommandHandler : IRequestHandler<RemoverCodigoValidacaoAutenticacaoCommand, bool>
    {
        private readonly IRepositorioCache repositorioCache;

        public RemoverCodigoValidacaoAutenticacaoCommandHandler(IRepositorioCache repositorioCache)
        {
            this.repositorioCache = repositorioCache ?? throw new ArgumentNullException(nameof(repositorioCache));
        }

        public async Task<bool> Handle(RemoverCodigoValidacaoAutenticacaoCommand request, CancellationToken cancellationToken)
        {
            var chave = string.Format(CacheChave.Autenticacao, request.Codigo);
            await repositorioCache.RemoverRedisAsync(chave);
            return true;
        }
    }
}
