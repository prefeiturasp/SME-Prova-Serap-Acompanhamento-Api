using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterUesIdPorAnoLetivoModalidadeQuery : IRequest<string[]>
    {
        public ObterUesIdPorAnoLetivoModalidadeQuery(int anoLetivo, Modalidade modalidade, string[] uesId)
        {
            AnoLetivo = anoLetivo;
            Modalidade = modalidade;
            UesId = uesId;
        }

        public int AnoLetivo { get; set; }
        public Modalidade Modalidade { get; set; }
        public string[] UesId { get; set; }
    }
}