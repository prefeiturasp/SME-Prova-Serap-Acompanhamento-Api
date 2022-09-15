using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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