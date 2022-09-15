using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterProvaAlunoResultadoPorProvaIdAlunoRaQuery : IRequest<IEnumerable<ProvaAlunoResultado>>
    {
        public ObterProvaAlunoResultadoPorProvaIdAlunoRaQuery(long provaId, long alunoRa)
        {
            ProvaId = provaId;
            AlunoRa = alunoRa;
        }

        public long ProvaId { get; set; }
        public long AlunoRa { get; set; }
    }
}
