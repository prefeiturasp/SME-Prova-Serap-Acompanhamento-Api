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
                new QueryContainerDescriptor<Abrangencia>().Match(p => p.Field(f => f.GrupoId).Query(grupo));

            var result = await elasticClient.SearchAsync<Abrangencia>(s => s
                        .Index(IndexName)
                        .Query(_ => query));

            var abrangencias = result?.Hits.Select(t => t.Source).ToList();

            return abrangencias.Where(t => t.Login == login && t.GrupoId == grupo);
        }


        public async Task<Abrangencia> ObterPorUsuarioCoressoAsync(string UsuarioCoressoId)
        {
            UsuarioCoressoId = UsuarioCoressoId.ToLower();

            var query =
                new QueryContainerDescriptor<Abrangencia>().Match(p => p.Field(f => f.UsuarioId).Query(UsuarioCoressoId));

            var result = await elasticClient.SearchAsync<Abrangencia>(s => s
                        .Index(IndexName)
                        .Query(_ => query));

            var abrangencias = result?.Hits.Select(t => t.Source).ToList();

            return abrangencias.Where(t => t.UsuarioId == UsuarioCoressoId).FirstOrDefault();
        }
    }
}
