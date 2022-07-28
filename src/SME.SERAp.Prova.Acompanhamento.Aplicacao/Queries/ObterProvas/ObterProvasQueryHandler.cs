using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterProvasQueryHandler : IRequestHandler<ObterProvasQuery, IEnumerable<Dominio.Entities.Prova>>
    {
        private readonly IRepositorioProva repositorioProva;

        public ObterProvasQueryHandler(IRepositorioProva repositorioProva)
        {
            this.repositorioProva = repositorioProva ?? throw new ArgumentNullException(nameof(repositorioProva));
        }

        public async Task<IEnumerable<Dominio.Entities.Prova>> Handle(ObterProvasQuery request, CancellationToken cancellationToken)
        {
            return await repositorioProva.ObterProvaPorAnoLetivoSituacaoAsync(request.AnoLetivo, request.ProvaSituacao);
        }
    }
}
