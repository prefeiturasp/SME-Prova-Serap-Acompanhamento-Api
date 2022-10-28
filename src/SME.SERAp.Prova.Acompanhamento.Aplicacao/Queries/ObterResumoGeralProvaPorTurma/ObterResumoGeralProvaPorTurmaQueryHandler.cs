using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.Queries.ObterResumoGeralProvaPorTurma
{
   public class ObterResumoGeralProvaPorTurmaQueryHandler : IRequestHandler<ObterResumoGeralProvaPorTurmaQuery, ResumoGeralProvaDto>
    {
        private readonly IRepositorioProvaTurmaResultado repositorioProvaTurmaResultado;

        public ObterResumoGeralProvaPorTurmaQueryHandler(IRepositorioProvaTurmaResultado repositorioProvaTurmaResultado)
        {
            this.repositorioProvaTurmaResultado = repositorioProvaTurmaResultado ?? throw new ArgumentNullException(nameof(repositorioProvaTurmaResultado));
        }

        public async Task<ResumoGeralProvaDto> Handle(ObterResumoGeralProvaPorTurmaQuery request, CancellationToken cancellationToken)
        {
            return await repositorioProvaTurmaResultado.ObterResumoGeralPorTurmaAsync(request.Filtro, request.TurmaId, request.ProvaId, request.DresAbrangenciaId, request.UesAbrangenciaId, request.TurmasAbrangenciaId);

        }
    }
}