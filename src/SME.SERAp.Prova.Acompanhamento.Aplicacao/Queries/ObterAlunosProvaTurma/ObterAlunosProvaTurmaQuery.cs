using MediatR;
using SME.SERAp.Prova.Acompanhamento.Infra;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterAlunosProvaTurmaQuery : IRequest<IEnumerable<AlunoTurmaDto>>
    {
        public ObterAlunosProvaTurmaQuery(long provaId, long turmaId)
        {
            ProvaId = provaId;
            TurmaId = turmaId;
        }

        public long ProvaId { get; set; }
        public long TurmaId { get; set; }

    }
}
