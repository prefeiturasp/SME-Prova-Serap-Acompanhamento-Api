using MediatR;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class InserirTesteCommand : IRequest<bool>
    {
        public InserirTesteCommand(string descricao)
        {
            Descricao = descricao;
        }

        public string Descricao { get; set; }
    }
}
