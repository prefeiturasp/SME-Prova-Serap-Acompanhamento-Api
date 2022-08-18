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
            var anoLetivo = request.Filtro.AnoLetivo;
            var situacao = request.Filtro.ProvaSituacao;
            var provaIds = request.Filtro.ProvasId;
            var modalidade = request.Filtro.Modalidade;
            var dreId = request.Filtro.DreId;
            var ueId = request.Filtro.UeId != null ? Convert.ToInt32(request.Filtro.UeId) : (int?)null;
            var anoEscolar = request.Filtro.AnoEscolar != null ? Convert.ToString(request.Filtro.AnoEscolar) : string.Empty;
            var turmaId = request.Filtro.TurmaId;

            var resumoGeralProvas = new List<ResumoGeralProvaDto>();

            var provas = await repositorioProva.ObterProvaPorAnoLetivoSituacaoAsync(anoLetivo, situacao);

            if (provaIds != null && provaIds.Any())
                provas = provas.Where(p => provaIds.Any(x => x.ToString() == p.Id));

            if (modalidade != null)
                provas = provas.Where(p => p.Modalidade == modalidade);

            foreach (var prova in provas)
            {
                var resultadoProva = await ObterResumoFiltro(long.Parse(prova.Id), dreId, ueId, anoEscolar, turmaId);
                if (resultadoProva != null && resultadoProva.Any())
                {
                    resumoGeralProvas.Add(ObterResumoProva(resultadoProva));
                }
            }

            return resumoGeralProvas.AsEnumerable();
        }

        private async Task<IEnumerable<ProvaTurmaResultado>> ObterResumoFiltro(long provaId, int? dreId, int? ueId, string anoEscolar, long? turmaId)
        {
            return await repositorioProvaTurmaResultado.ObterResumoGeralPorFiltroAsync(provaId, dreId, ueId, anoEscolar, turmaId);
        }

        private ResumoGeralProvaDto ObterResumoProva(IEnumerable<ProvaTurmaResultado> resultadoProva)
        {
            var resumoProva = new ResumoGeralProvaDto();
            var prova = resultadoProva.FirstOrDefault();
            resumoProva.ProvaId = prova.ProvaId;
            resumoProva.TituloProva = resultadoProva.Select(p => p.Descricao).FirstOrDefault();
            resumoProva.TotalAlunos = resultadoProva.Sum(p => p.TotalAlunos);
            resumoProva.ProvasIniciadas = resultadoProva.Sum(p => p.TotalIniciadas);
            resumoProva.ProvasNaoFinalizadas = resultadoProva.Sum(p => p.TotalNaoFinalizados);
            resumoProva.ProvasFinalizadas = resultadoProva.Sum(p => p.TotalFinalizados);
            resumoProva.TempoMedio = ObterTempoMedio(resultadoProva);
            resumoProva.PercentualRealizado = ObterPencentualRealizadoResumoProva(resumoProva.ProvasFinalizadas, resumoProva.TotalAlunos);
            resumoProva.DetalheProva = ObterDetalheProva(resultadoProva);
            return resumoProva;
        }

        private int ObterTempoMedio(IEnumerable<ProvaTurmaResultado> resultadoProva)
        {
            var somaTempoMedio = resultadoProva.Sum(p => p.TempoMedio);
            var qtde = resultadoProva.Count();
            var media = somaTempoMedio / qtde;
            return Convert.ToInt32(media);
        }

        private DetalheProvaDto ObterDetalheProva(IEnumerable<ProvaTurmaResultado> resultadoProva)
        {
            var detalhe = new DetalheProvaDto();
            var prova = resultadoProva.FirstOrDefault();
            detalhe.DataInicio = prova.Inicio;
            detalhe.DataFim = prova.Fim;
            detalhe.QtdeQuestoesProva = Convert.ToInt32(prova.QuantidadeQuestoes);
            detalhe.TotalQuestoes = resultadoProva.Sum(p => p.TotalQuestoes);
            detalhe.Respondidas = resultadoProva.Sum(p => p.QuestoesRespondidas);
            detalhe.PercentualRespondido = ObterPercentualRespondidoDetalheProva(detalhe.TotalQuestoes, detalhe.Respondidas);
            return detalhe;
        }

        private decimal ObterPencentualRealizadoResumoProva(long provasFinalizadas, long totalAlunos)
        {
            var percentual = (provasFinalizadas * 100) / totalAlunos;
            return percentual > 100 ? 100 : Math.Round(Convert.ToDecimal(percentual), 2);
        }

        private decimal ObterPercentualRespondidoDetalheProva(decimal totalQuestoes, decimal respondidas)
        {
            var percentual = (respondidas * 100) / totalQuestoes;
            percentual = Convert.ToDecimal(percentual);
            return percentual > 100 ? 100 : Math.Round(percentual, 2);
        }
    }
}
