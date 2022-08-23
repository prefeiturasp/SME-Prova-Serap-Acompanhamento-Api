using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Infra.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterAbrangenciaPorCodigoValidacaoAutenticacaoQueryHandler : IRequestHandler<ObterAbrangenciaPorCodigoValidacaoAutenticacaoQuery, IEnumerable<Abrangencia>>
    {
        private readonly IRepositorioCache repositorioCache;

        public ObterAbrangenciaPorCodigoValidacaoAutenticacaoQueryHandler(IRepositorioCache repositorioCache)
        {
            this.repositorioCache = repositorioCache ?? throw new ArgumentNullException(nameof(repositorioCache));
        }

        public async Task<IEnumerable<Abrangencia>> Handle(ObterAbrangenciaPorCodigoValidacaoAutenticacaoQuery request, CancellationToken cancellationToken)
        {
            var chave = string.Format(CacheChave.Autenticacao, request.Codigo);

            var abrangencias = await repositorioCache.ObterRedisAsync<List<Abrangencia>>(chave);

            if (abrangencias == null || !abrangencias.Any()) return default;

            return abrangencias;
        }
    }
}
