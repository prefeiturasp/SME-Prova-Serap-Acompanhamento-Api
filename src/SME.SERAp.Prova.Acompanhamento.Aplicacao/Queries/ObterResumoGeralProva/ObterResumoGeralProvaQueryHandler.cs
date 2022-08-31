using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterResumoGeralProvaQueryHandler : IRequestHandler<ObterResumoGeralProvaQuery, ResumoGeralDto>
    {
        private readonly IRepositorioProva repositorioProva;
        private readonly IRepositorioProvaTurmaResultado repositorioProvaTurmaResultado;
        
        public ObterResumoGeralProvaQueryHandler(IRepositorioProva repositorioProva, IRepositorioProvaTurmaResultado repositorioProvaTurmaResultado)
        {
            this.repositorioProva = repositorioProva ?? throw new ArgumentNullException(nameof(repositorioProva));
            this.repositorioProvaTurmaResultado = repositorioProvaTurmaResultado ?? throw new ArgumentNullException(nameof(repositorioProvaTurmaResultado));
        }

        public async Task<ResumoGeralDto> Handle(ObterResumoGeralProvaQuery request, CancellationToken cancellationToken)
        {
            var resumoGeral = new ResumoGeralDto();

            var provas = await repositorioProva.ObterProvaPorAnoLetivoSituacaoAsync(request.Filtro.AnoLetivo, request.Filtro.ProvaSituacao);

            if(request.Filtro.ProvasId != null && request.Filtro.ProvasId.Any())
            {
                provas = provas.Where(p => request.Filtro.ProvasId.Any(n => n.ToString() == p.Id));
            }

            provas = provas.OrderBy(o => o.Descricao);

            var resumoGeralProvas = new List<ResumoGeralProvaDto>();
            foreach (var prova in provas)
            {
                var resumoGeralProva = await repositorioProvaTurmaResultado.ObterResumoGeralPorFiltroAsync(request.Filtro, long.Parse(prova.Id), request.DresId, request.UesId);

                resumoGeralProva.ProvaId = long.Parse(prova.Id);
                resumoGeralProva.TituloProva = prova.Descricao;
                resumoGeralProva.DetalheProva.DataInicio = prova.Inicio;
                resumoGeralProva.DetalheProva.DataFim = prova.Fim;
                resumoGeralProva.TempoMedio = ObterTempoMedio(resumoGeralProva);

                if (resumoGeralProva != null && resumoGeralProva.TotalAlunos > 0)
                    resumoGeralProvas.Add(resumoGeralProva);
            }

            resumoGeral.TotalRegistros = resumoGeralProvas.Count();
            resumoGeral.TotalPaginas = (int)Math.Ceiling((double)resumoGeral.TotalRegistros / request.NumeroRegistros);

            var skip = (request.NumeroPagina - 1) * request.NumeroRegistros;
            resumoGeral.Items = resumoGeralProvas.Skip(skip).Take(request.NumeroRegistros).ToList();

            return resumoGeral;
        }

        private long ObterTempoMedio(ResumoGeralProvaDto resumoGeralProva)
        {
            if (resumoGeralProva.TotalTempoMedio == 0) return 0;
            return (int)(resumoGeralProva.TotalTempoMedio / resumoGeralProva.TotalTurmas);
        }
    }
}
