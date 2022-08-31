using Nest;
using SME.SERAp.Prova.Acompanhamento.Dominio;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
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
                q.Term(t => t.Field(f => f.TurmaId).Value(turmaId)) &&
                !q.Term(t => t.Field(f => f.AlunoSituacao).Value(99))
                ).Size(10000);

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
    }
}
