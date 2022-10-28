using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.Queries.ObterResumoGeralProvaPorUe
{
    class ObterResumoGeralProvaPorUeQueryHandler : IRequestHandler<ObterResumoGeralProvaPorUeQuery, ResumoGeralProvaDto>
    {
        private readonly IRepositorioProvaTurmaResultado repositorioProvaTurmaResultado;

        public ObterResumoGeralProvaPorUeQueryHandler(IRepositorioProvaTurmaResultado repositorioProvaTurmaResultado)
        {
            this.repositorioProvaTurmaResultado = repositorioProvaTurmaResultado ?? throw new ArgumentNullException(nameof(repositorioProvaTurmaResultado));
        }

        public async Task<ResumoGeralProvaDto> Handle(ObterResumoGeralProvaPorUeQuery request, CancellationToken cancellationToken)
        {
            return await repositorioProvaTurmaResultado.ObterResumoGeralPorUeAsync(request.Filtro, request.UeId, request.ProvaId, request.DresAbrangenciaId, request.UesAbrangenciaId, request.TurmasAbrangenciaId);

        }
    }
}