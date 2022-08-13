using Nest;
using SME.SERAp.Prova.Acompanhamento.Dominio;
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
    }
}
