using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class InserirTesteCommandHandler : IRequestHandler<InserirTesteCommand, bool>
    {
        private readonly IRepositorioTeste repositorioTeste;

        public InserirTesteCommandHandler(IRepositorioTeste repositorioTeste)
        {
            this.repositorioTeste = repositorioTeste ?? throw new ArgumentException(nameof(repositorioTeste));
        }

        public async Task<bool> Handle(InserirTesteCommand request, CancellationToken cancellationToken)
        {
            await repositorioTeste.CriarIndexAsync();
            return await repositorioTeste.InserirAsync(new Teste(request.Descricao));
        }
    }
}
