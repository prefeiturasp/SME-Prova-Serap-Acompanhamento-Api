using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.Queries
{
    public class ObterAnosPorModalidadeEscolasQueryHandler : IRequestHandler<ObterAnosPorModalidadeEscolasQuery, IEnumerable<Ano>>
    {
        private readonly IRepositorioAno repositorioAno;

        public ObterAnosPorModalidadeEscolasQueryHandler(IRepositorioAno repositorioAno)
        {
            this.repositorioAno = repositorioAno ?? throw new ArgumentNullException(nameof(repositorioAno));
        }

        public async Task<IEnumerable<Ano>> Handle(ObterAnosPorModalidadeEscolasQuery request, CancellationToken cancellationToken)
        {
            return await repositorioAno.ObterPorAnoLetivoModalidadeAsync(request.Modalidade, request.UeId);
        }
    }
}
