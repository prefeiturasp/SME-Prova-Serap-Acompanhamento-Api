using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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