using Nest;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using SME.SERAp.Prova.Acompanhamento.Infra;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories
{
    public class RepositorioProvaTurmaResultado : RepositorioBase<ProvaTurmaResultado>, IRepositorioProvaTurmaResultado
    {
        public RepositorioProvaTurmaResultado(ElasticOptions elasticOptions, IElasticClient elasticClient) : base(elasticOptions, elasticClient)
        {
        }

        private static QueryContainer MontarQueryFiltro(FiltroDto filtro, long[] dresId, long[] uesId, long[] turmasId)
        {
            var now = DateTime.Now.ToString("yyyy-MM-ddT00:00:00.000'Z'");

            QueryContainer query = new QueryContainerDescriptor<ProvaTurmaResultado>().Term(p => p.Field(p => p.AnoLetivo).Value(filtro.AnoLetivo));

            if (dresId != null && dresId.Any())
            {
                QueryContainer queryDres = new QueryContainerDescriptor<ProvaTurmaResultado>();

                foreach (var id in dresId)
                    queryDres = queryDres || new QueryContainerDescriptor<ProvaTurmaResultado>().Term(d => d.Field(f => f.DreId).Value(id));

                query = query && (queryDres);
            }

            if (uesId != null && uesId.Any())
            {
                QueryContainer queryUes = new QueryContainerDescriptor<ProvaTurmaResultado>();

                foreach (var id in uesId)
                    queryUes = queryUes || new QueryContainerDescriptor<ProvaTurmaResultado>().Term(d => d.Field(f => f.UeId).Value(id));

                query = query && (queryUes);
            }

            if (turmasId != null && turmasId.Any())
            {
                QueryContainer queryTurmas = new QueryContainerDescriptor<ProvaTurmaResultado>();

                foreach (var id in turmasId)
                    queryTurmas = queryTurmas || new QueryContainerDescriptor<ProvaTurmaResultado>().Term(d => d.Field(f => f.TurmaId).Value(id));

                query = query && (queryTurmas);
            }

            if (filtro.ProvaSituacao == ProvaSituacao.EmAndamento)
            {
                query = query
                    && new QueryContainerDescriptor<ProvaTurmaResultado>().DateRange(d => d.Field(f => f.Inicio).LessThanOrEquals(now))
                    && new QueryContainerDescriptor<ProvaTurmaResultado>().DateRange(d => d.Field(f => f.Fim).GreaterThanOrEquals(now));
            }
            else if (filtro.ProvaSituacao == ProvaSituacao.Concluida)
            {
                query = query
                    && new QueryContainerDescriptor<ProvaTurmaResultado>().DateRange(d => d.Field(f => f.Fim).LessThan(now));
            }

            if (filtro.DreId != null && filtro.DreId > 0)
                query = query && new QueryContainerDescriptor<ProvaTurmaResultado>().Term(p => p.Field(p => p.DreId).Value(filtro.DreId));
            if (filtro.Modalidade != null && filtro.Modalidade > 0)
                query = query && new QueryContainerDescriptor<ProvaTurmaResultado>().Term(p => p.Field(p => p.Modalidade).Value(filtro.Modalidade));
            if (filtro.UeId != null && filtro.UeId > 0)
                query = query && new QueryContainerDescriptor<ProvaTurmaResultado>().Term(p => p.Field(p => p.UeId).Value(filtro.UeId));
            if (filtro.TurmaId != null && filtro.TurmaId > 0)
                query = query && new QueryContainerDescriptor<ProvaTurmaResultado>().Term(p => p.Field(p => p.TurmaId).Value(filtro.TurmaId));
            if (filtro.AnoEscolar != null && filtro.AnoEscolar > 0)
                query = query && new QueryContainerDescriptor<ProvaTurmaResultado>().Term(p => p.Field(p => p.Ano).Value(filtro.AnoEscolar.ToString()));

            QueryContainer queryProva = new QueryContainerDescriptor<ProvaTurmaResultado>();

            if (filtro.ProvasId != null && filtro.ProvasId.Any())
            {
                foreach (var provaId in filtro.ProvasId)
                    queryProva = queryProva || new QueryContainerDescriptor<ProvaTurmaResultado>().Term(p => p.Field(p => p.ProvaId).Value(provaId));

                query = query && (queryProva);
            }

            return query;
        }

        public async Task<ResumoGeralProvaDto> ObterResumoGeralPorFiltroAsync(FiltroDto filtro, long provaId, long[] dresId, long[] uesId, long[] turmasId)
        {
            QueryContainer query = MontarQueryFiltro(filtro, dresId, uesId, turmasId);

            query = query && new QueryContainerDescriptor<ProvaTurmaResultado>().Term(p => p.Field(p => p.ProvaId).Value(provaId));

            var resultado = new List<ProvaTurmaResultado>();

            var search = new SearchDescriptor<ProvaTurmaResultado>(IndexName)
                .Query(_ => query)
                .Size(0)
                .Aggregations(a => a.Sum("TotalAlunos", s => s.Field(f => f.TotalAlunos))
                    && a.Sum("ProvasIniciadas", s => s.Field(f => f.TotalIniciadas))
                    && a.Sum("ProvasNaoFinalizadas", s => s.Field(f => f.TotalNaoFinalizados))
                    && a.Sum("ProvasFinalizadas", s => s.Field(f => f.TotalFinalizados))
                    && a.Sum("TotalTempoMedio", s => s.Field(f => f.TempoMedio))
                    && a.Min("QtdeQuestoesProva", s => s.Field(f => f.QuantidadeQuestoes))
                    && a.Sum("TotalQuestoes", s => s.Field(f => f.TotalQuestoes))
                    && a.Sum("Respondidas", s => s.Field(f => f.QuestoesRespondidas)));

            var response = await elasticClient.SearchAsync<ProvaTurmaResultado>(search);
            if (!response.IsValid) return default;

            var totalTurmas = await ObterTotalTurmas(query);

            var resumoGeralProvaDto = new ResumoGeralProvaDto()
            {
                TotalAlunos = Convert.ToInt64(response.Aggregations.ValueCount("TotalAlunos").Value.GetValueOrDefault()),
                ProvasIniciadas = Convert.ToInt64(response.Aggregations.ValueCount("ProvasIniciadas").Value.GetValueOrDefault()),
                ProvasNaoFinalizadas = Convert.ToInt64(response.Aggregations.ValueCount("ProvasNaoFinalizadas").Value.GetValueOrDefault()),
                ProvasFinalizadas = Convert.ToInt64(response.Aggregations.ValueCount("ProvasFinalizadas").Value.GetValueOrDefault()),
                TotalTempoMedio = Convert.ToInt64(response.Aggregations.ValueCount("TotalTempoMedio").Value.GetValueOrDefault()),
                DetalheProva = new DetalheProvaDto()
                {
                    QtdeQuestoesProva = Convert.ToInt64(response.Aggregations.ValueCount("QtdeQuestoesProva").Value.GetValueOrDefault()),
                    TotalQuestoes = Convert.ToDecimal(response.Aggregations.ValueCount("TotalQuestoes").Value.GetValueOrDefault()),
                    Respondidas = Convert.ToDecimal(response.Aggregations.ValueCount("Respondidas").Value.GetValueOrDefault()),
                },
                TotalTurmas = (long)Math.Ceiling(totalTurmas)
            };

            return resumoGeralProvaDto;
        }

        public async Task<double> ObterTotalTurmas(QueryContainer query)
        {
            var searchTotalTurmas = new SearchDescriptor<ProvaTurmaResultado>(IndexName)
                .Query(q => !q.Term(p => p.TempoMedio, 0) && query)
                .Size(0)
                .Aggregations(a => a.ValueCount("TotalTurmas", c => c.Field(p => p.TurmaId)));

            var responseTotalTurmas = await elasticClient.SearchAsync<ProvaTurmaResultado>(searchTotalTurmas);
            if (!responseTotalTurmas.IsValid) return 0;

            return responseTotalTurmas.Aggregations.ValueCount("TotalTurmas").Value.GetValueOrDefault();
        }

        public async Task<double> ObterTotalProvasPorFiltroAsync(FiltroDto filtro, long[] dresId, long[] uesId, long[] turmasId)
        {
            QueryContainer query = MontarQueryFiltro(filtro, dresId, uesId, turmasId);

            var search = new SearchDescriptor<ProvaTurmaResultado>(IndexName)
           .Query(_ => query).Size(0)
           .Aggregations(a => a.Sum("TotalProvas", s => s.Field(f => f.TotalAlunos)));

            var response = await elasticClient.SearchAsync<ProvaTurmaResultado>(search);
            if (!response.IsValid) return default;

            return response.Aggregations.ValueCount("TotalProvas").Value.GetValueOrDefault();
        }

        public async Task<double> ObterTotalProvasIniciadasHojePorFiltroAsync(FiltroDto filtro, long[] dresId, long[] uesId, long[] turmasId)
        {
            QueryContainer query = MontarQueryFiltro(filtro, dresId, uesId, turmasId);

            var search = new SearchDescriptor<ProvaTurmaResultado>(IndexName)
           .Query(_ => query).Size(0)
           .Aggregations(a => a.Sum("TotalIniciadas", s => s.Field(f => f.TotalIniciadas)));

            var response = await elasticClient.SearchAsync<ProvaTurmaResultado>(search);
            if (!response.IsValid) return default;

            return response.Aggregations.ValueCount("TotalIniciadas").Value.GetValueOrDefault();
        }

        public async Task<double> ObterTotalProvasNaoFinalizadasPorFiltroAsync(FiltroDto filtro, long[] dresId, long[] uesId, long[] turmasId)
        {
            QueryContainer query = MontarQueryFiltro(filtro, dresId, uesId, turmasId);

            var search = new SearchDescriptor<ProvaTurmaResultado>(IndexName)
           .Query(_ => query).Size(0)
           .Aggregations(a => a.Sum("TotalNaoFinalizado", s => s.Field(f => f.TotalNaoFinalizados)));

            var response = await elasticClient.SearchAsync<ProvaTurmaResultado>(search);
            if (!response.IsValid) return default;

            return response.Aggregations.ValueCount("TotalNaoFinalizado").Value.GetValueOrDefault();
        }

        public async Task<double> ObterTotalProvasFinalizadasPorFiltroAsync(FiltroDto filtro, long[] dresId, long[] uesId, long[] turmasId)
        {
            QueryContainer query = MontarQueryFiltro(filtro, dresId, uesId, turmasId);

            var search = new SearchDescriptor<ProvaTurmaResultado>(IndexName)
           .Query(_ => query).Size(0)
           .Aggregations(a => a.Sum("TotalFinalizados", s => s.Field(f => f.TotalFinalizados)));

            var response = await elasticClient.SearchAsync<ProvaTurmaResultado>(search);
            if (!response.IsValid) return default;

            return response.Aggregations.ValueCount("TotalFinalizados").Value.GetValueOrDefault();
        }
    }
}
