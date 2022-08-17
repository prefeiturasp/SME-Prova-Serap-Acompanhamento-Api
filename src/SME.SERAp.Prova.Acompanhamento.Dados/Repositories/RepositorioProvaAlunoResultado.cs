using Nest;
using SME.SERAp.Prova.Acompanhamento.Dominio;
using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados
{
    public class RepositorioProvaAlunoResultado : RepositorioBase<ProvaAlunoResultado>, IRepositorioProvaAlunoResultado
    {

        protected override string IndexName => "prova-aluno-resultado";

        public RepositorioProvaAlunoResultado(IElasticClient elasticClient) : base(elasticClient)
        {

        }

        public async Task<IEnumerable<ProvaAlunoResultado>> ObterPorProvaTurmaAsync(long provaId, long turmaId)
        {
            var search = new SearchDescriptor<ProvaAlunoResultado>(IndexName).Query(q =>
                q.Term(t => t.Field(f => f.ProvaId).Value(provaId)) &&
                q.Term(t => t.Field(f => f.TurmaId).Value(turmaId))
                );

            var response = await elasticClient.SearchAsync<ProvaAlunoResultado>(search);

            if (!response.IsValid) return default;

            return response.Hits.Select(hit => hit.Source).ToList();
        }

        public async Task<IEnumerable<ProvaAlunoResultado>> ObterProvaAlunoResultadoPorFiltroAsync(FiltroDto filtro)
        {
            QueryContainer query = new QueryContainerDescriptor<ProvaAlunoResultado>().Term(p => p.Field(p => p.AnoLetivo).Value(filtro.AnoLetivo));

            if (filtro.DreId != null && filtro.DreId > 0)
                query = query && new QueryContainerDescriptor<ProvaAlunoResultado>().Term(p => p.Field(p => p.DreId).Value(filtro.DreId));
            if (filtro.Modalidade != null && filtro.Modalidade > 0)
                query = query && new QueryContainerDescriptor<ProvaAlunoResultado>().Term(p => p.Field(p => p.Modalidade).Value(filtro.Modalidade));
            if (filtro.UeId != null && filtro.UeId > 0)
                query = query && new QueryContainerDescriptor<ProvaAlunoResultado>().Term(p => p.Field(p => p.UeId).Value(filtro.UeId));
            if (filtro.TurmaId != null && filtro.TurmaId > 0)
                query = query && new QueryContainerDescriptor<ProvaAlunoResultado>().Term(p => p.Field(p => p.TurmaId).Value(filtro.TurmaId));
            if (filtro.AnoEscolar != null && filtro.AnoEscolar > 0)
                query = query && new QueryContainerDescriptor<ProvaAlunoResultado>().Term(p => p.Field(p => p.Ano).Value(filtro.AnoEscolar.ToString()));

            if (filtro.ProvasId != null && filtro.ProvasId.Any())
            {
                foreach (var provaId in filtro.ProvasId)
                    query = query && new QueryContainerDescriptor<ProvaAlunoResultado>().Term(p => p.Field(p => p.ProvaId).Value(provaId));
            }

            var search = new SearchDescriptor<ProvaAlunoResultado>(IndexName)
           .Query(_ => query)
           .From(0)
           .Size(10000);

            var response = await elasticClient.SearchAsync<ProvaAlunoResultado>(search);
            if (!response.IsValid) return default;

            return response.Hits.Select(hit => hit.Source).ToList();
        }

        private static QueryContainer MontarQueryFiltro(FiltroDto filtro)
        {
            var now = DateTime.Now;

            QueryContainer query = new QueryContainerDescriptor<ProvaAlunoResultado>().Term(p => p.Field(p => p.AnoLetivo).Value(filtro.AnoLetivo));

            if (filtro.ProvaSituacao == ProvaSituacao.EmAndamento)
            {
                query = query
                    && new QueryContainerDescriptor<ProvaAlunoResultado>().DateRange(d => d.Field(f => f.Inicio).LessThanOrEquals(now))
                    && new QueryContainerDescriptor<ProvaAlunoResultado>().DateRange(d => d.Field(f => f.Fim).GreaterThanOrEquals(now));
            }
            else if (filtro.ProvaSituacao == ProvaSituacao.Concluida)
            {
                query = query
                    && new QueryContainerDescriptor<ProvaAlunoResultado>().DateRange(d => d.Field(f => f.Fim).LessThan(now));
            }

            if (filtro.DreId != null && filtro.DreId > 0)
                query = query && new QueryContainerDescriptor<ProvaAlunoResultado>().Term(p => p.Field(p => p.DreId).Value(filtro.DreId));
            if (filtro.Modalidade != null && filtro.Modalidade > 0)
                query = query && new QueryContainerDescriptor<ProvaAlunoResultado>().Term(p => p.Field(p => p.Modalidade).Value(filtro.Modalidade));
            if (filtro.UeId != null && filtro.UeId > 0)
                query = query && new QueryContainerDescriptor<ProvaAlunoResultado>().Term(p => p.Field(p => p.UeId).Value(filtro.UeId));
            if (filtro.TurmaId != null && filtro.TurmaId > 0)
                query = query && new QueryContainerDescriptor<ProvaAlunoResultado>().Term(p => p.Field(p => p.TurmaId).Value(filtro.TurmaId));
            if (filtro.AnoEscolar != null && filtro.AnoEscolar > 0)
                query = query && new QueryContainerDescriptor<ProvaAlunoResultado>().Term(p => p.Field(p => p.Ano).Value(filtro.AnoEscolar.ToString()));

            if (filtro.ProvasId != null && filtro.ProvasId.Any())
            {
                foreach (var provaId in filtro.ProvasId)
                    query = query && new QueryContainerDescriptor<ProvaAlunoResultado>().Term(p => p.Field(p => p.ProvaId).Value(provaId));
            }

            return query;
        }

        public async Task<double> ObterTotalProvasPorFiltroAsync(FiltroDto filtro)
        {
            QueryContainer query = MontarQueryFiltro(filtro);

            var search = new SearchDescriptor<ProvaAlunoResultado>(IndexName)
           .Query(_ => query).Size(0)
           .Aggregations(a => a.ValueCount("TotalProvas", s => s.Field(f => f.AlunoId)));

            var response = await elasticClient.SearchAsync<ProvaAlunoResultado>(search);
            if (!response.IsValid) return default;

            return response.Aggregations.ValueCount("TotalProvas").Value.GetValueOrDefault();
        }

        public async Task<double> ObterTotalProvasIniciadasHojePorFiltroAsync(FiltroDto filtro)
        {
            QueryContainer query = MontarQueryFiltro(filtro);

            var dataInicio = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 00:00:01"));
            var dataFim = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));

            query = query
                && new QueryContainerDescriptor<ProvaAlunoResultado>().DateRange(d => d.Field(f => f.AlunoInicio).GreaterThanOrEquals(dataInicio))
                && new QueryContainerDescriptor<ProvaAlunoResultado>().DateRange(d => d.Field(f => f.AlunoInicio).LessThanOrEquals(dataFim));

            query = query && !new QueryContainerDescriptor<ProvaAlunoResultado>().Exists(d => d.Field(d => d.AlunoFim));

            var search = new SearchDescriptor<ProvaAlunoResultado>(IndexName)
           .Query(_ => query).Size(0)
           .Aggregations(a => a.ValueCount("TotalIniciadas", s => s.Field(f => f.AlunoId)));

            var response = await elasticClient.SearchAsync<ProvaAlunoResultado>(search);
            if (!response.IsValid) return default;

            return response.Aggregations.ValueCount("TotalIniciadas").Value.GetValueOrDefault();
        }

        public async Task<double> ObterTotalProvasNaoFinalizadasPorFiltroAsync(FiltroDto filtro)
        {
            QueryContainer query = MontarQueryFiltro(filtro);

            var dataInicio = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));

            query = query && new QueryContainerDescriptor<ProvaAlunoResultado>().DateRange(d => d.Field(f => f.AlunoInicio).LessThan(dataInicio));
            query = query && !new QueryContainerDescriptor<ProvaAlunoResultado>().Exists(d => d.Field(d => d.AlunoFim));

            var search = new SearchDescriptor<ProvaAlunoResultado>(IndexName)
           .Query(_ => query).Size(0)
           .Aggregations(a => a.ValueCount("TotalNaoFinalizado", s => s.Field(f => f.AlunoId)));

            var response = await elasticClient.SearchAsync<ProvaAlunoResultado>(search);
            if (!response.IsValid) return default;

            return response.Aggregations.ValueCount("TotalNaoFinalizado").Value.GetValueOrDefault();
        }

        public async Task<double> ObterTotalProvasFinalizadasPorFiltroAsync(FiltroDto filtro)
        {
            QueryContainer query = MontarQueryFiltro(filtro);

            query = query && new QueryContainerDescriptor<ProvaAlunoResultado>().Exists(d => d.Field(d => d.AlunoFim));

            var search = new SearchDescriptor<ProvaAlunoResultado>(IndexName)
           .Query(_ => query).Size(0)
           .Aggregations(a => a.ValueCount("TotalFinalizados", s => s.Field(f => f.AlunoId)));

            var response = await elasticClient.SearchAsync<ProvaAlunoResultado>(search);
            if (!response.IsValid) return default;

            return response.Aggregations.ValueCount("TotalFinalizados").Value.GetValueOrDefault();
        }
    }
}
