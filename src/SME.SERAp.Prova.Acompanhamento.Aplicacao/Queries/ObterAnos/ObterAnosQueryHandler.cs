using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterAnosQueryHandler : IRequestHandler<ObterAnosQuery, IEnumerable<Ano>>
    {
        private readonly IRepositorioAno repositorioAno;

        public ObterAnosQueryHandler(IRepositorioAno repositorioAno)
        {
            this.repositorioAno = repositorioAno ?? throw new ArgumentNullException(nameof(repositorioAno));
        }

        public async Task<IEnumerable<Ano>> Handle(ObterAnosQuery request, CancellationToken cancellationToken)
        {
            return await repositorioAno.ObterPorAnoLetivoUeIdAsync(request.AnoLetivo, request.UeId);
        }
    }
}
