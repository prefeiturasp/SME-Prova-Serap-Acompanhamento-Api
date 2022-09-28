using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio;
using System.Collections.Generic;

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
