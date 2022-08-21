using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterUesQuery : IRequest<IEnumerable<Ue>>
    {
        public ObterUesQuery(long dreId, long[] ids)
        {
            DreId = dreId;
            Ids = ids;
        }

        public long DreId { get; set; }
        public long[] Ids { get; set; }
    }
}
