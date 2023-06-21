using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class AlterarProvaAlunoResultadoCommandHandler : IRequestHandler<AlterarProvaAlunoResultadoCommand, bool>
    {
        private readonly IRepositorioProvaAlunoResultado repositorioProvaAlunoResultado;

        public AlterarProvaAlunoResultadoCommandHandler(IRepositorioProvaAlunoResultado repositorioProvaAlunoResultado)
        {
            this.repositorioProvaAlunoResultado = repositorioProvaAlunoResultado ?? throw new ArgumentNullException(nameof(repositorioProvaAlunoResultado));
        }

        public async Task<bool> Handle(AlterarProvaAlunoResultadoCommand request, CancellationToken cancellationToken)
        {
            return await repositorioProvaAlunoResultado.AlterarAsync(request.ProvaTurmaAlunoSituacao);
        }
    }
}
