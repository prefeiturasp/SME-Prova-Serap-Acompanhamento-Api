using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterDresQueryHandler : IRequestHandler<ObterDresQuery, IEnumerable<Dre>>
    {
        private readonly IRepositorioDre repositorioDre;

        public ObterDresQueryHandler(IRepositorioDre repositorioDre)
        {
            this.repositorioDre = repositorioDre ?? throw new ArgumentNullException(nameof(repositorioDre));
        }

        public async Task<IEnumerable<Dre>> Handle(ObterDresQuery request, CancellationToken cancellationToken)
        {
            if (request.Ids != null && request.Ids.Any())
                return await repositorioDre.obterPorIdsAsync(request.Ids);
            else
                return await repositorioDre.ObterTodasAsync();
        }
    }
}
