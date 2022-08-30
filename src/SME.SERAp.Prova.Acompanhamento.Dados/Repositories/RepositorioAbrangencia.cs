using Nest;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories
{
    public class RepositorioAbrangencia : RepositorioBase<Abrangencia>, IRepositorioAbrangencia
    {
        public RepositorioAbrangencia(ElasticOptions elasticOptions, IElasticClient elasticClient) : base(elasticOptions, elasticClient)
        {
        }

        public async Task<IEnumerable<Abrangencia>> ObterPorLoginGrupoAsync(string login, string grupo)
        {
            var query =
                new QueryContainerDescriptor<Abrangencia>().Match(p => p.Field(p => p.Login).Query(login)) &&
                new QueryContainerDescriptor<Abrangencia>().Match(p => p.Field(p => p.CoressoId).Query(grupo.ToLower()));

            var result = await elasticClient.SearchAsync<Abrangencia>(s => s
                        .Index(IndexName)
                        .Query(_ => query));

            return result?.Documents.ToList();
        }
    }
}
