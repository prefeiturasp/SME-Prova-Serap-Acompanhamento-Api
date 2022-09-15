using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterProvaPorIdQuery : IRequest<Dominio.Entities.Prova>
    {
        public ObterProvaPorIdQuery(long provaId)
        {
            ProvaId = provaId;
        }

        public long ProvaId { get; set; }
    }
}