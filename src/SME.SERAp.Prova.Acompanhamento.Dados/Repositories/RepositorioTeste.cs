using Nest;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories
{
    public class RepositorioTeste : RepositorioBase<Teste>, IRepositorioTeste
    {
        public override string IndexName => "teste";
        public RepositorioTeste(IElasticClient elasticClient) : base(elasticClient)
        {
        }
    }
}
