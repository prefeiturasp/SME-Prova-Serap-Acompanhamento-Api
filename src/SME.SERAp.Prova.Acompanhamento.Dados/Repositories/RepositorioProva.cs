using Nest;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories
{
    public class RepositorioProva : RepositorioBase<Dominio.Entities.Prova>, IRepositorioProva
    {
        protected override string IndexName => "prova";
        public RepositorioProva(IElasticClient elasticClient) : base(elasticClient) { }

        public async Task<IEnumerable<Dominio.Entities.Prova>> ObterProvaPorAnoLetivoSituacaoAsync(int anoLetivo, ProvaSituacao provaSituacao)
        {
            QueryContainer query = new QueryContainerDescriptor<Dominio.Entities.Prova>().Term(p => p.Field(p => p.Ano).Value(anoLetivo));

            var now = DateTime.Now;
            if (provaSituacao != ProvaSituacao.NaoCadastrado)
            {
                if (provaSituacao == ProvaSituacao.EmAndamento)
                {
                    query = query
                        && new QueryContainerDescriptor<Dominio.Entities.Prova>().DateRange(d => d.Field(f => f.Inicio).LessThanOrEquals(now))
                        && new QueryContainerDescriptor<Dominio.Entities.Prova>().DateRange(d => d.Field(f => f.Fim).GreaterThanOrEquals(now));
                }
                else if (provaSituacao == ProvaSituacao.Concluida)
                {
                    query = query
                        && new QueryContainerDescriptor<Dominio.Entities.Prova>().DateRange(d => d.Field(f => f.Fim).LessThan(now));
                }
            }

            var search = new SearchDescriptor<Dominio.Entities.Prova>(IndexName)
                .Query(_ => query)
                .From(0)
                .Size(10000);

            var response = await elasticClient.SearchAsync<Dominio.Entities.Prova>(search);

            if (!response.IsValid) return default;

            return response.Hits.Select(hit => hit.Source).ToList();
        }

        public async Task<IEnumerable<Dominio.Entities.Prova>> ObterProvaPorAnoEModalidadeAsync(Modalidade modalidade, int anoLetivo)
        {
            QueryContainer query = new QueryContainerDescriptor<Dominio.Entities.Prova>().Term(p => p.Field(p => p.Ano).Value(anoLetivo));

            if (modalidade != Modalidade.NaoCadastrado)
                query = query && new QueryContainerDescriptor<Dominio.Entities.Prova>().Term(p => p.Field(p => p.Modalidade).Value(modalidade));

            var search = new SearchDescriptor<Dominio.Entities.Prova>(IndexName)
             .Query(_ => query)
             .From(0)
             .Size(10000);

            var response = await elasticClient.SearchAsync<Dominio.Entities.Prova>(search);

            if (!response.IsValid) return default;

            return response.Hits.Select(hit => hit.Source).ToList();
        }

        public async Task<IEnumerable<Dominio.Entities.Prova>> ObterProvaPorAnoAsync(int anoLetivo)
        {
            QueryContainer query = new QueryContainerDescriptor<Dominio.Entities.Prova>().Term(p => p.Field(p => p.Ano).Value(anoLetivo));

            var search = new SearchDescriptor<Dominio.Entities.Prova>(IndexName)
             .Query(_ => query)
             .From(0)
             .Size(10000);

            var response = await elasticClient.SearchAsync<Dominio.Entities.Prova>(search);

            if (!response.IsValid) return default;
            return response.Hits.Select(hit => hit.Source).ToList();
        }
    }
}
