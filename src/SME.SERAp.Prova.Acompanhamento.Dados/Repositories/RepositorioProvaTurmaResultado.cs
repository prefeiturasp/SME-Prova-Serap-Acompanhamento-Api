using Nest;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories
{
    public class RepositorioProvaTurmaResultado : RepositorioBase<ProvaTurmaResultado>, IRepositorioProvaTurmaResultado
    {
        protected override string IndexName => "prova-turma-resultado";
        public RepositorioProvaTurmaResultado(IElasticClient elasticClient) : base(elasticClient) { }

        public async Task<IEnumerable<ProvaTurmaResultado>> ObterResumoGeralPorFiltroAsync(long provaId, int? dreId, int? ueId, string anoEscolar, long? turmaId)
        {
            QueryContainer query = new QueryContainerDescriptor<ProvaTurmaResultado>().Term(p => p.Field(p => p.ProvaId).Value(provaId));

            if (dreId != null)
                query = query && new QueryContainerDescriptor<ProvaTurmaResultado>().Term(p => p.Field(p => p.DreId).Value(dreId));

            if (ueId != null)
                query = query && new QueryContainerDescriptor<ProvaTurmaResultado>().Term(p => p.Field(p => p.UeId).Value(ueId));

            if (!string.IsNullOrEmpty(anoEscolar))
                query = query && new QueryContainerDescriptor<ProvaTurmaResultado>().Term(p => p.Field(p => p.Ano).Value(anoEscolar));

            if (turmaId != null)
                query = query && new QueryContainerDescriptor<ProvaTurmaResultado>().Term(p => p.Field(p => p.TurmaId).Value(turmaId));

            var search = new SearchDescriptor<ProvaTurmaResultado>(IndexName)
                .Query(_ => query)
                .From(0)
                .Size(10000);

            var response = await elasticClient.SearchAsync<ProvaTurmaResultado>(search);

            if (!response.IsValid) return default;

            return response.Hits.Select(hit => hit.Source).AsEnumerable();
        }

        private static QueryContainer MontarQueryFiltro(FiltroDto filtro)
        {
            var now = DateTime.Now;

            QueryContainer query = new QueryContainerDescriptor<ProvaTurmaResultado>().Term(p => p.Field(p => p.AnoLetivo).Value(filtro.AnoLetivo));

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

            if (filtro.ProvasId != null && filtro.ProvasId.Any())
            {
                foreach (var provaId in filtro.ProvasId)
                    query = query && new QueryContainerDescriptor<ProvaTurmaResultado>().Term(p => p.Field(p => p.ProvaId).Value(provaId));
            }

            return query;
        }

        public async Task<double> ObterTotalProvasPorFiltroAsync(FiltroDto filtro)
        {
            QueryContainer query = MontarQueryFiltro(filtro);

            var search = new SearchDescriptor<ProvaTurmaResultado>(IndexName)
           .Query(_ => query).Size(0)
           .Aggregations(a => a.Sum("TotalProvas", s => s.Field(f => f.TotalAlunos)));

            var response = await elasticClient.SearchAsync<ProvaTurmaResultado>(search);
            if (!response.IsValid) return default;

            return response.Aggregations.ValueCount("TotalProvas").Value.GetValueOrDefault();
        }

        public async Task<double> ObterTotalProvasIniciadasHojePorFiltroAsync(FiltroDto filtro)
        {
            QueryContainer query = MontarQueryFiltro(filtro);

            var search = new SearchDescriptor<ProvaTurmaResultado>(IndexName)
           .Query(_ => query).Size(0)
           .Aggregations(a => a.Sum("TotalIniciadas", s => s.Field(f => f.TotalIniciadas)));

            var response = await elasticClient.SearchAsync<ProvaTurmaResultado>(search);
            if (!response.IsValid) return default;

            return response.Aggregations.ValueCount("TotalIniciadas").Value.GetValueOrDefault();
        }

        public async Task<double> ObterTotalProvasNaoFinalizadasPorFiltroAsync(FiltroDto filtro)
        {
            QueryContainer query = MontarQueryFiltro(filtro);

            var search = new SearchDescriptor<ProvaTurmaResultado>(IndexName)
           .Query(_ => query).Size(0)
           .Aggregations(a => a.Sum("TotalNaoFinalizado", s => s.Field(f => f.TotalNaoFinalizados)));

            var response = await elasticClient.SearchAsync<ProvaTurmaResultado>(search);
            if (!response.IsValid) return default;

            return response.Aggregations.ValueCount("TotalNaoFinalizado").Value.GetValueOrDefault();
        }

        public async Task<double> ObterTotalProvasFinalizadasPorFiltroAsync(FiltroDto filtro)
        {
            QueryContainer query = MontarQueryFiltro(filtro);

            var search = new SearchDescriptor<ProvaTurmaResultado>(IndexName)
           .Query(_ => query).Size(0)
           .Aggregations(a => a.Sum("TotalFinalizados", s => s.Field(f => f.TotalFinalizados)));

            var response = await elasticClient.SearchAsync<ProvaTurmaResultado>(search);
            if (!response.IsValid) return default;

            return response.Aggregations.ValueCount("TotalFinalizados").Value.GetValueOrDefault();
        }
    }
}
