using Nest;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories
{
    public class RepositorioTurma : RepositorioBase<Turma>, IRepositorioTurma
    {
        public RepositorioTurma(ElasticOptions elasticOptions, IElasticClient elasticClient) : base(elasticOptions, elasticClient)
        {
        }

        public async Task<IEnumerable<Turma>> ObterPorUeIdAnoAsync(int anoLetivo, long ueId, Modalidade modalidade, string ano, long[] ids)
        {
            QueryContainer query =
                new QueryContainerDescriptor<Turma>().Term(p => p.Field(p => p.AnoLetivo).Value(anoLetivo)) &&
                new QueryContainerDescriptor<Turma>().Term(p => p.Field(p => p.UeId).Value(ueId)) &&
                new QueryContainerDescriptor<Turma>().Term(p => p.Field(p => p.Ano).Value(ano)) &&
                new QueryContainerDescriptor<Turma>().Term(p => p.Field(p => p.Modalidade).Value(modalidade));

            if (ids != null && ids.Any())
            {
                if (ids[0] > 0)
                {
                    QueryContainer queryIds = new QueryContainerDescriptor<Turma>();
                    foreach (var id in ids)
                        queryIds = queryIds || new QueryContainerDescriptor<Turma>().Term(p => p.Field(p => p.Id).Value(id));
                    query = query && (queryIds);
                }
            }

            var search = new SearchDescriptor<Turma>(IndexName).Query(q => query).From(0).Size(10000);

            var response = await elasticClient.SearchAsync<Turma>(search);

            if (!response.IsValid) return default;

            return response.Hits.Select(hit => hit.Source).ToList();
        }


        public async Task<IEnumerable<Turma>> ObterPorUeIdAsync(int anoLetivo, long ueId)
        {
            QueryContainer query =
                new QueryContainerDescriptor<Turma>().Term(p => p.Field(p => p.AnoLetivo).Value(anoLetivo)) &&
                new QueryContainerDescriptor<Turma>().Term(p => p.Field(p => p.UeId).Value(ueId));



            var search = new SearchDescriptor<Turma>(IndexName).Query(q => query).From(0).Size(10000);

            var response = await elasticClient.SearchAsync<Turma>(search);

            if (!response.IsValid) return default;

            return response.Hits.Select(hit => hit.Source).ToList();
        }
    }
}

