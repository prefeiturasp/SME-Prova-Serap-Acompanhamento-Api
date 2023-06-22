using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterResumoGeralProvaPorDreQueryHandler : IRequestHandler<ObterResumoGeralProvaPorDreQuery, ResumoGeralProvaDto>
    {
        private readonly IRepositorioProvaTurmaResultado repositorioProvaTurmaResultado;

        public ObterResumoGeralProvaPorDreQueryHandler(IRepositorioProva repositorioProva, IRepositorioProvaTurmaResultado repositorioProvaTurmaResultado)
        {
            this.repositorioProvaTurmaResultado = repositorioProvaTurmaResultado ?? throw new ArgumentNullException(nameof(repositorioProvaTurmaResultado));
        }

        public async Task<ResumoGeralProvaDto> Handle(ObterResumoGeralProvaPorDreQuery request, CancellationToken cancellationToken)
        {
            return await repositorioProvaTurmaResultado.ObterResumoGeralPorDreAsync(request.Filtro, request.DreId, request.ProvaId, request.DresAbrangenciaId, request.UesAbrangenciaId, request.TurmasAbrangenciaId);
        }
    }
}