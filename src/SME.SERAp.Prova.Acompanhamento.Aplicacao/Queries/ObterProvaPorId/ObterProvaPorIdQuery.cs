using MediatR;

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