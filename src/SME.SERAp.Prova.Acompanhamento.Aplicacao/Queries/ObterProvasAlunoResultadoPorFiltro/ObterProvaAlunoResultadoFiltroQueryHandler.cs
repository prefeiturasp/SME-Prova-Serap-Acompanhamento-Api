using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados;
using SME.SERAp.Prova.Acompanhamento.Dominio;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterProvaAlunoResultadoFiltroQueryHandler : IRequestHandler<ObterProvaAlunoResultadoFiltroQuery, IEnumerable<ProvaAlunoResultado>>
    {
        private readonly IRepositorioProvaAlunoResultado repositorioAlunoResultado;

        public ObterProvaAlunoResultadoFiltroQueryHandler(IRepositorioProvaAlunoResultado repositorioAlunoResultado)
        {
            this.repositorioAlunoResultado = repositorioAlunoResultado ?? throw new ArgumentNullException(nameof(repositorioAlunoResultado));
        }

        public async Task<IEnumerable<ProvaAlunoResultado>> Handle(ObterProvaAlunoResultadoFiltroQuery request, CancellationToken cancellationToken)
        {
            return await repositorioAlunoResultado.ObterProvaAlunoResultadoPorFiltroAsync(request.Filtro);
        }
    }
}