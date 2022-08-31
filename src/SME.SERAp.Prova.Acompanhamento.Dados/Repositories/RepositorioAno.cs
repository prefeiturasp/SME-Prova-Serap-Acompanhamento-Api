using Nest;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories
{
    public class RepositorioAno : RepositorioBase<Ano>, IRepositorioAno
    {
        protected override string IndexName => "ano";
        public RepositorioAno(IElasticClient elasticClient) : base(elasticClient)
        {
        }

        public async Task<IEnumerable<Ano>> ObterPorAnoLetivoModalidadeUeIdAsync(int anoLetivo, Modalidade modalidade, long ueId)
        {
            var search = new SearchDescriptor<Ano>(IndexName).Query(q =>
                q.Term(t => t.Field(f => f.AnoLetivo).Value(anoLetivo)) &&
                q.Term(t => t.Field(f => f.Modalidade).Value(modalidade)) &&
                q.Term(t => t.Field(f => f.UeId).Value(ueId))
            ).From(0).Size(10000);
            var response = await elasticClient.SearchAsync<Ano>(search);

            if (!response.IsValid) return default;

            return response.Hits.Select(hit => hit.Source).ToList();
        }
    }
}
