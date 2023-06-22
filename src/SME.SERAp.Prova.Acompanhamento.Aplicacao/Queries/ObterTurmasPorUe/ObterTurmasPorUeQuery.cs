using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.Queries.ObterTurmasPorUe
{
    public class ObterTurmasPorUeQuery : IRequest<IEnumerable<Turma>>
    {
        public ObterTurmasPorUeQuery(int anoLetivo, long ueId)
        {
            AnoLetivo = anoLetivo;
            UeId = ueId;

        }

        public int AnoLetivo { get; set; }
        public long UeId { get; set; }

    }
}