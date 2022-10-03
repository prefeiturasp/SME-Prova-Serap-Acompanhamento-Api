using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados;
using SME.SERAp.Prova.Acompanhamento.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterAlunosProvaTurmaQueryHandler : IRequestHandler<ObterAlunosProvaTurmaQuery, IEnumerable<AlunoTurmaDto>>
    {

        private readonly IRepositorioProvaAlunoResultado repositorioProvaAlunoResultado;

        public ObterAlunosProvaTurmaQueryHandler(IRepositorioProvaAlunoResultado repositorioProvaAlunoResultado)
        {
            this.repositorioProvaAlunoResultado = repositorioProvaAlunoResultado ?? throw new ArgumentNullException(nameof(repositorioProvaAlunoResultado));
        }

        public async Task<IEnumerable<AlunoTurmaDto>> Handle(ObterAlunosProvaTurmaQuery request, CancellationToken cancellationToken)
        {
            var dados = await repositorioProvaAlunoResultado.ObterPorProvaTurmaAsync(request.ProvaId, request.TurmaId);
            if (dados == null || !dados.Any()) return default;
            return dados.Select(x => new AlunoTurmaDto(x.AlunoNome, x.AlunoDownload, x.AlunoRa, x.AlunoInicio, x.AlunoFim, x.AlunoTempoMedio, x.AlunoQuestaoRespondida, x.Situacao, x.UsuarioIdReabertura, x.DataHoraReabertura));
        }
    }
}
