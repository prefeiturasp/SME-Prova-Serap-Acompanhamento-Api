using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterAbrangenciaPorLoginGrupoQueryHandler : IRequestHandler<ObterAbrangenciaPorLoginGrupoQuery, IEnumerable<Abrangencia>>
    {
        private readonly IRepositorioAbrangencia repositorioAbrangencia;

        public ObterAbrangenciaPorLoginGrupoQueryHandler(IRepositorioAbrangencia repositorioAbrangencia)
        {
            this.repositorioAbrangencia = repositorioAbrangencia ?? throw new ArgumentNullException(nameof(repositorioAbrangencia));
        }

        public async Task<IEnumerable<Abrangencia>> Handle(ObterAbrangenciaPorLoginGrupoQuery request, CancellationToken cancellationToken)
        {
            return await repositorioAbrangencia.ObterPorLoginGrupoAsync(request.Login, request.Perfil);
        }
    }
}
