using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
   public class ObterNomeUsuarioPorIdCoressoQueryHandler : IRequestHandler<ObterNomeUsuarioPorIdCoressoQuery, Abrangencia>
    {
        private readonly IRepositorioAbrangencia repositorioAbrangencia;

        public ObterNomeUsuarioPorIdCoressoQueryHandler(IRepositorioAbrangencia repositorioAbrangencia)
        {
            this.repositorioAbrangencia = repositorioAbrangencia ?? throw new ArgumentNullException(nameof(repositorioAbrangencia));
        }

        public async Task<Abrangencia> Handle(ObterNomeUsuarioPorIdCoressoQuery request, CancellationToken cancellationToken)
        {
            return await repositorioAbrangencia.ObterPorUsuarioCoressoAsync(request.UsuarioCoressoId);
        }
    }
}
