using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterAnosQuery : IRequest<IEnumerable<Ano>>
    {
        public ObterAnosQuery(int anoLetivo, Modalidade modalidade, long ueId)
        {
            AnoLetivo = anoLetivo;
            Modalidade = modalidade;
            UeId = ueId;
        }

        public int AnoLetivo { get; set; }
        public Modalidade Modalidade { get; set; }
        public long UeId { get; set; }
    }
}
