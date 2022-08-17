using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.Queries.ObterProvasPorProvaIdAnoLetivoSituacao
{
   public class ObterProvasPorIdsAnoLetivoSituacaoQueryHandler : IRequestHandler<ObterProvasPorIdsAnoLetivoSituacaoQuery, IEnumerable<Dominio.Entities.Prova>>
    {
        private readonly IRepositorioProva repositorioProva;

        public ObterProvasPorIdsAnoLetivoSituacaoQueryHandler(IRepositorioProva repositorioProva)
        {
            this.repositorioProva = repositorioProva ?? throw new ArgumentNullException(nameof(repositorioProva));
        }

        public async Task<IEnumerable<Dominio.Entities.Prova>> Handle(ObterProvasPorIdsAnoLetivoSituacaoQuery request, CancellationToken cancellationToken)
        {
            return await repositorioProva.ObterProvaPorProvaIdAnoLetivoSituacaoAsync(request.ProvasId, request.AnoLetivo, request.ProvaSituacao, request.Modalidade);
        }
    }
}
