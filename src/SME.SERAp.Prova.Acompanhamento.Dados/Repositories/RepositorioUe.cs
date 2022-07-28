using Nest;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories
{
    public class RepositorioUe : RepositorioBase<Ue>, IRepositorioUe
    {
        protected override string IndexName => "ue";
        public RepositorioUe(IElasticClient elasticClient) : base(elasticClient)
        {
        }

        public async Task<IEnumerable<Ue>> ObterPorIdsAsync(long dreId, long[] ids)
        {
            QueryContainer query = new QueryContainerDescriptor<Ue>().Term(p => p.Field(p => p.DreId).Value(dreId));

            foreach (var id in ids)
                query = query && new QueryContainerDescriptor<Ue>().Term(p => p.Field(p => p.Id).Value(id));

            var search = new SearchDescriptor<Ue>(IndexName).Query(_ => query).From(0).Size(10000);

            var response = await elasticClient.SearchAsync<Ue>(search);

            if (!response.IsValid) return default;

            return response.Hits.Select(hit => hit.Source).ToList();
        }
    }
}
