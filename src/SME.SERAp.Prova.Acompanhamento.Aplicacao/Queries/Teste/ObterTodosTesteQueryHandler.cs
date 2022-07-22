using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterTodosTesteQueryHandler : IRequestHandler<ObterTodosTesteQuery, IEnumerable<Teste>>
    {
        private readonly IRepositorioTeste repositorioTeste;

        public ObterTodosTesteQueryHandler(IRepositorioTeste repositorioTeste)
        {
            this.repositorioTeste = repositorioTeste ?? throw new ArgumentException(nameof(repositorioTeste));
        }

        public async Task<IEnumerable<Teste>> Handle(ObterTodosTesteQuery request, CancellationToken cancellationToken)
        {
            await repositorioTeste.CriarIndexAsync();
            return await repositorioTeste.ObterTodosAsync();
        }
    }
}
