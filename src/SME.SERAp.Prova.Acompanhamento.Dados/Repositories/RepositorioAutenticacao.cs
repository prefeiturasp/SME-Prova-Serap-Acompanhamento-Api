using Nest;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories
{
    public class RepositorioAutenticacao : RepositorioBase<Autenticacao>, IRepositorioAutenticacao
    {
        protected override string IndexName => "autenticacao";
        public RepositorioAutenticacao(IElasticClient elasticClient) : base(elasticClient)
        {
        }

        public async Task<Autenticacao> ObterPorCodigoAsync(string codigo)
        {
            var query = new QueryContainerDescriptor<Autenticacao>()
                .Match(p => p.Field(p => p.Codigo).Query(codigo.ToLower()));

            var result = await elasticClient.SearchAsync<Autenticacao>(s => s
                        .Index(IndexName)
                        .Query(_ => query));

            return result?.Documents.FirstOrDefault();
        }

        public async Task<bool> DeletarPorCodigoAsync(string codigo)
        {
            var response = await elasticClient.DeleteByQueryAsync<Autenticacao>(del => del
                .Index(IndexName)
                .Query(t => t.Match(p => p.Field(x => x.Codigo).Query(codigo))));

            if (!response.IsValid) return false;

            return true;
        }
    }
}
