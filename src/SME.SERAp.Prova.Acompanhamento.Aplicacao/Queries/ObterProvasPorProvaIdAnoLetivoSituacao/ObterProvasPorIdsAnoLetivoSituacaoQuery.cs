using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.Queries.ObterProvasPorProvaIdAnoLetivoSituacao
{
    public class ObterProvasPorIdsAnoLetivoSituacaoQuery : IRequest<IEnumerable<Dominio.Entities.Prova>>
    {
        public ObterProvasPorIdsAnoLetivoSituacaoQuery(int[] provasId, int anoLetivo, ProvaSituacao provaSituacao, Modalidade? modalidade)
        {
            AnoLetivo = anoLetivo;
            ProvaSituacao = provaSituacao;
        }

        public int AnoLetivo { get; set; }
        public ProvaSituacao ProvaSituacao { get; set; }
        public int[] ProvasId { get; set; }
        public Modalidade? Modalidade { get; set; }
    }
}