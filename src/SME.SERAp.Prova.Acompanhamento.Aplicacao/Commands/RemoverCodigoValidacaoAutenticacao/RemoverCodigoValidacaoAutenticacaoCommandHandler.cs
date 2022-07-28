using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class RemoverCodigoValidacaoAutenticacaoCommandHandler : IRequestHandler<RemoverCodigoValidacaoAutenticacaoCommand, bool>
    {
        private readonly IRepositorioAutenticacao repositorioAutenticacao;

        public RemoverCodigoValidacaoAutenticacaoCommandHandler(IRepositorioAutenticacao repositorioAutenticacao)
        {
            this.repositorioAutenticacao = repositorioAutenticacao ?? throw new ArgumentNullException(nameof(repositorioAutenticacao));
        }

        public async Task<bool> Handle(RemoverCodigoValidacaoAutenticacaoCommand request, CancellationToken cancellationToken)
        {
            return await repositorioAutenticacao.DeletarPorCodigoAsync(request.Codigo);
        }
    }
}
