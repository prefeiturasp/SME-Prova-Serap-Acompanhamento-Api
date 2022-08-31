using Nest;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories
{
    public class RepositorioUe : RepositorioBase<Ue>, IRepositorioUe
    {
        public RepositorioUe(ElasticOptions elasticOptions, IElasticClient elasticClient) : base(elasticOptions, elasticClient)
        {
        }

        public async Task<IEnumerable<Ue>> ObterPorIdsAsync(long dreId, long[] ids)
        {
            QueryContainer query = new QueryContainerDescriptor<Ue>().Term(p => p.Field(p => p.DreId).Value(dreId));

            if (ids != null && ids.Any())
            {
                if (ids[0] > 0)
                {
                    QueryContainer queryIds = new QueryContainerDescriptor<Ue>();
                    foreach (var id in ids)
                        queryIds = queryIds || new QueryContainerDescriptor<Ue>().Term(p => p.Field(p => p.Id).Value(id));
                    query = query && (queryIds);
                }
            }

            var search = new SearchDescriptor<Ue>(IndexName).Query(_ => query).From(0).Size(10000);

            var response = await elasticClient.SearchAsync<Ue>(search);

            if (!response.IsValid) return default;

            return response.Hits.Select(hit => hit.Source).ToList();
        }
    }
}
