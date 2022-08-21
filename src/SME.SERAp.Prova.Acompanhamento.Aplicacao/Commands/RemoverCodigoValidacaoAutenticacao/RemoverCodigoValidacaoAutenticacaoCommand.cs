using MediatR;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class RemoverCodigoValidacaoAutenticacaoCommand : IRequest<bool>
    {
        public RemoverCodigoValidacaoAutenticacaoCommand(string codigo)
        {
            Codigo = codigo;
        }

        public string Codigo { get; set; }
    }
}
