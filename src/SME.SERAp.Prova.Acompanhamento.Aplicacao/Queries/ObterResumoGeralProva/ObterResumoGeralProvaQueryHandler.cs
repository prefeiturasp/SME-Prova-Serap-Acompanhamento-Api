using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using SME.SERAp.Prova.Acompanhamento.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterResumoGeralProvaQueryHandler : IRequestHandler<ObterResumoGeralProvaQuery, IEnumerable<ResumoGeralProvaDto>>
    {

        private readonly IRepositorioProvaTurmaResultado repositorioProvaTurmaResultado;
        private readonly IRepositorioProva repositorioProva;

        public ObterResumoGeralProvaQueryHandler(IRepositorioProvaTurmaResultado repositorioProvaTurmaResultado, IRepositorioProva repositorioProva)
        {
            this.repositorioProvaTurmaResultado = repositorioProvaTurmaResultado ?? throw new ArgumentNullException(nameof(repositorioProvaTurmaResultado));
            this.repositorioProva = repositorioProva ?? throw new ArgumentNullException(nameof(repositorioProva));
        }

        public async Task<IEnumerable<ResumoGeralProvaDto>> Handle(ObterResumoGeralProvaQuery request, CancellationToken cancellationToken)
        {
            var provaIds = new long[] { 137 };
            var modalidade = Modalidade.EJA;

            var provas = await repositorioProva.ObterProvaPorAnoLetivoSituacaoAsync(2022, ProvaSituacao.EmAndamento);

            if (provaIds != null && provaIds.Any())
                provas = provas.Where(p => provaIds.Any(x => x.ToString() == p.Id));

            if (modalidade != null)
                provas = provas.Where(p => p.Modalidade == modalidade);

            var dreId = 1;
            var ueId = 8;
            var turmaId = 0;

            foreach (var prova in provas)
            {
                var resultProva = await repositorioProvaTurmaResultado.ObterResumoGeralPorFiltroAsync(long.Parse(prova.Id), dreId, ueId, "", turmaId);
            }

            return default;
        }
    }
}
