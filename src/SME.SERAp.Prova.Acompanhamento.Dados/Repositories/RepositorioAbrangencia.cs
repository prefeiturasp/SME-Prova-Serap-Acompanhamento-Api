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
            grupo = grupo.ToLower();

            var query =
                new QueryContainerDescriptor<Abrangencia>().Match(p => p.Field(f => f.Login).Query(login)) &&
                new QueryContainerDescriptor<Abrangencia>().Term(p => p.Field(f => f.GrupoId.Suffix("keyword")).Value(grupo));

            var result = await elasticClient.SearchAsync<Abrangencia>(s => s
                        .Index(IndexName)
                        .Query(_ => query)
                        .Size(1000));

            var abrangencias = result?.Hits.Select(t => t.Source).ToList();

            return abrangencias.Where(t => t.Login == login && t.GrupoId == grupo);
        }


        public async Task<Abrangencia> ObterPorUsuarioCoressoAsync(string UsuarioCoressoId)
        {
            UsuarioCoressoId = UsuarioCoressoId.ToLower();

            var query =
                new QueryContainerDescriptor<Abrangencia>().Term(p => p.Field(f => f.UsuarioId.Suffix("keyword")).Value(UsuarioCoressoId));

            var result = await elasticClient.SearchAsync<Abrangencia>(s => s
                        .Index(IndexName)
                        .Query(_ => query)
                        .Size(1000));

            var abrangencias = result?.Hits.Select(t => t.Source).ToList();

            return abrangencias.FirstOrDefault(t => t.UsuarioId == UsuarioCoressoId);
        }
    }
}
