using Nest;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
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
    }
}
