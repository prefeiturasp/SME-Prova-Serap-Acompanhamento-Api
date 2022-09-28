using MediatR;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class AlterarProvaAlunoResultadoCommand : IRequest<bool>
    {
        public AlterarProvaAlunoResultadoCommand(Dominio.ProvaAlunoResultado provaAlunoResultado)
        {
            ProvaTurmaAlunoSituacao = provaAlunoResultado;
        }

        public Dominio.ProvaAlunoResultado ProvaTurmaAlunoSituacao { get; set; }
    }
}