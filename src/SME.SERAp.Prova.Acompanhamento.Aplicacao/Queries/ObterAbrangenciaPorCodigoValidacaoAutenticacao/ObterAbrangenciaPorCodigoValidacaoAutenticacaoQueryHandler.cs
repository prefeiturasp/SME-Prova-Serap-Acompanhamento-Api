using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterAbrangenciaPorCodigoValidacaoAutenticacaoQueryHandler : IRequestHandler<ObterAbrangenciaPorCodigoValidacaoAutenticacaoQuery, IEnumerable<Abrangencia>>
    {
        private readonly IRepositorioAutenticacao repositorioAutenticacao;

        public ObterAbrangenciaPorCodigoValidacaoAutenticacaoQueryHandler(IRepositorioAutenticacao repositorioAutenticacao)
        {
            this.repositorioAutenticacao = repositorioAutenticacao ?? throw new ArgumentNullException(nameof(repositorioAutenticacao));
        }

        public async Task<IEnumerable<Abrangencia>> Handle(ObterAbrangenciaPorCodigoValidacaoAutenticacaoQuery request, CancellationToken cancellationToken)
        {
            var autenticacao = await repositorioAutenticacao.ObterPorCodigoAsync(request.Codigo);

            if (autenticacao == null) return default;

            return autenticacao.Abrangencias;
        }
    }
}
