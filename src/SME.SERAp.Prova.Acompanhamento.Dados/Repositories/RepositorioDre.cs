using Nest;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories
{
    public class RepositorioDre : RepositorioBase<Dre>, IRepositorioDre
    {
        protected override string IndexName => "dre";
        public RepositorioDre(IElasticClient elasticClient) : base(elasticClient)
        {
        }

        public async Task<IEnumerable<Dre>> obterPorIdsAsync(long[] ids)
        {
            QueryContainer query = new QueryContainerDescriptor<Dre>();

            foreach (var id in ids)
                query = query && new QueryContainerDescriptor<Dre>().Term(p => p.Field(p => p.Id).Value(id));


            var search = new SearchDescriptor<Ue>(IndexName).Query(_ => query).From(0).Size(10000);

            var response = await elasticClient.SearchAsync<Dre>(search);

            if (!response.IsValid) return default;

            return response.Hits.Select(hit => hit.Source).ToList();
        }
    }
}
