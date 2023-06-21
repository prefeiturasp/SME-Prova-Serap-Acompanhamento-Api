using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterProvaPorIdQueryHandler : IRequestHandler<ObterProvaPorIdQuery, Dominio.Entities.Prova>
    {
        private readonly IRepositorioProva repositorioProva;

        public ObterProvaPorIdQueryHandler(IRepositorioProva repositorioProva)
        {
            this.repositorioProva = repositorioProva ?? throw new ArgumentNullException(nameof(repositorioProva));
        }

        public async Task<Dominio.Entities.Prova> Handle(ObterProvaPorIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioProva.ObterPorIdAsync(request.ProvaId);
        }
    }
}