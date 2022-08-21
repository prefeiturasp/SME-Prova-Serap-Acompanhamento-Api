using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class ObterProvaUseCase : AbstractUseCase, IObterProvaUseCase
    {
        public ObterProvaUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<IEnumerable<SelecioneDto>> Executar(int anoLetivo, ProvaSituacao situacao)
        {
            var provas = await mediator.Send(new ObterProvasQuery(anoLetivo, situacao));
            if (provas == null && !provas.Any()) return default;

            return provas.Select(s => new SelecioneDto(s.Id, s.Descricao)).OrderBy(o => o.Descricao);
        }
    }
}
