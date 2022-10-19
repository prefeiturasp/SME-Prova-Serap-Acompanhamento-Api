using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.Queries
{
    public class ObterUesIdPorAnoLetivoModalidadeQueryHandler : IRequestHandler<ObterUesIdPorAnoLetivoModalidadeQuery, string[]>
    {
        private readonly IRepositorioAno repositorioAno;

        public ObterUesIdPorAnoLetivoModalidadeQueryHandler(IRepositorioAno repositorioAno)
        {
            this.repositorioAno = repositorioAno ?? throw new ArgumentNullException(nameof(repositorioAno));
        }

        public async Task<string[]> Handle(ObterUesIdPorAnoLetivoModalidadeQuery request, CancellationToken cancellationToken)
        {
            return await repositorioAno.ObterUesIdPorAnoLetivoModalidadeAsync(request.AnoLetivo, request.Modalidade, request.UesId);
        }
    }
}
