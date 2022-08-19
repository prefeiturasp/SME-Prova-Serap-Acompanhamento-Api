using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases;
using SME.SERAp.Prova.Acompanhamento.Infra;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterResumoGeralProvasUseCase : AbstractUseCase, IObterResumoGeralProvasUseCase
    {
        public ObterResumoGeralProvasUseCase(IMediator mediator) : base(mediator) { }

        public async Task<ResumoGeralDto> Executar(FiltroDto filtro)
        {
            var numeroPagina = filtro.NumeroPagina;
            var numeroRegistros = filtro.NumeroRegistros;
            var resumoGeralProvas = await mediator.Send(new ObterResumoGeralProvaQuery(filtro));
            var resumoGeral = new ResumoGeralDto();

            if (numeroPagina <= 1)
                resumoGeral.Items = resumoGeralProvas.Take(numeroRegistros).ToList();

            var skip = (numeroPagina - 1) * numeroRegistros;
            resumoGeral.Items = resumoGeralProvas.Skip(skip).Take(numeroRegistros).ToList();
            resumoGeral.TotalRegistros = resumoGeralProvas.Count();
            resumoGeral.TotalPaginas = (int)Math.Ceiling((double)resumoGeral.TotalRegistros / numeroRegistros);

            return resumoGeral;
        }
    }
}
