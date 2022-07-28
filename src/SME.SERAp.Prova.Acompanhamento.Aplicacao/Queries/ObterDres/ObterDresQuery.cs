using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterDresQuery : IRequest<IEnumerable<Dre>>
    {
        public ObterDresQuery(long[] ids)
        {
            Ids = ids;
        }

        public long[] Ids { get; set; }
    }
}
